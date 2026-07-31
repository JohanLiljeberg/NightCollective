using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

namespace Night.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;

        // Mobile-first optimized breakpoints
        private const int WidthTiny = 320;      // Old phones, portrait
        private const int WidthSmall = 640;     // Modern phones @2x, portrait
        private const int WidthMedium = 1024;   // Tablets @2x, portrait
        private const int WidthLarge = 1600;    // Desktop @2x
        private const int WidthXLarge = 2400;   // Retina displays @2x

        // WebP quality settings optimized for mobile
        private const int QualitySmall = 85;    // Higher quality for small images (more noticeable compression)
        private const int QualityMedium = 80;   // Balanced quality/size
        private const int QualityLarge = 75;    // Lower quality acceptable on large screens

        public ImageService(IWebHostEnvironment environment, HttpClient httpClient)
        {
            _environment = environment;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Generates a semantic filename based on upload time and a short hash.
        /// Format: yyyyMMdd_HHmmss_XXXX (e.g., 20250612_143022_a3f2)
        /// </summary>
        private string GenerateBaseFileName()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
            var hash = GenerateShortHash();
            return $"{timestamp}_{hash}";
        }

        /// <summary>
        /// Generates a short 4-character hash from a random value.
        /// </summary>
        private string GenerateShortHash()
        {
            var randomBytes = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return BitConverter.ToString(randomBytes).Replace("-", "").ToLower().Substring(0, 4);
        }

        public async Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type)
        {
            if (file == null || file.Length == 0) return null;

            string subFolder = type switch
            {
                ImageType.Games => "images/games",
                ImageType.Developers => "images/developers",
                ImageType.Events => "images/events",
                _ => "images/misc"
            };

            var targetFolder = Path.Combine(_environment.WebRootPath, subFolder);
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            var baseFileName = GenerateBaseFileName();
            var imageFolder = Path.Combine(targetFolder, baseFileName);
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            var smallName = $"{baseFileName}_sm.webp";
            var mediumName = $"{baseFileName}_md.webp";
            var largeName = $"{baseFileName}_lg.webp";

            using var stream = file.OpenReadStream();
            using var image = await Image.LoadAsync(stream);

            // Save multiple sizes optimized for mobile-first
            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, smallName), WidthSmall, QualitySmall);
            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, mediumName), WidthMedium, QualityMedium);
            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, largeName), WidthLarge, QualityLarge);

            return new ImageSizeUrls(
                SmallUrl: $"/{subFolder}/{baseFileName}/{smallName}",
                MediumUrl: $"/{subFolder}/{baseFileName}/{mediumName}",
                LargeUrl: $"/{subFolder}/{baseFileName}/{largeName}"
            );
        }

        public async Task<ImageSizeUrls?> DownloadAndProcessUrlAsync(string imageUrl, ImageType type)
        {
            if (string.IsNullOrWhiteSpace(imageUrl)) return null;

            try
            {
                // Download the image from URL
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                if (imageBytes.Length == 0) return null;

                string subFolder = type switch
                {
                    ImageType.Games => "images/games",
                    ImageType.Developers => "images/developers",
                    ImageType.Events => "images/events",
                    _ => "images/misc"
                };

                var targetFolder = Path.Combine(_environment.WebRootPath, subFolder);
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                var baseFileName = GenerateBaseFileName();
                var imageFolder = Path.Combine(targetFolder, baseFileName);
                if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

                var smallName = $"{baseFileName}_sm.webp";
                var mediumName = $"{baseFileName}_md.webp";
                var largeName = $"{baseFileName}_lg.webp";

                // Load image from bytes
                using var memoryStream = new MemoryStream(imageBytes);
                using var image = await Image.LoadAsync(memoryStream);

                // Save resized versions with optimized quality
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, smallName), WidthSmall, QualitySmall);
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, mediumName), WidthMedium, QualityMedium);
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, largeName), WidthLarge, QualityLarge);

                return new ImageSizeUrls(
                    SmallUrl: $"/{subFolder}/{baseFileName}/{smallName}",
                    MediumUrl: $"/{subFolder}/{baseFileName}/{mediumName}",
                    LargeUrl: $"/{subFolder}/{baseFileName}/{largeName}"
                );
            }
            catch (Exception ex)
            {
                // Log the error and return null
                Console.WriteLine($"Error downloading and processing image from URL {imageUrl}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Resizes and saves an image as WebP with optimized quality settings for mobile performance.
        /// Uses Lanczos3 resampler for best quality/performance balance.
        /// </summary>
        private async Task SaveResizedWebPAsync(Image sourceImage, string outputPath, int targetWidth, int quality)
        {
            using var clonedImage = sourceImage.Clone(ctx =>
            {
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(targetWidth, 0),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Lanczos3, // Best quality for downscaling
                    Compand = true // Better color accuracy
                });
            });

            var encoder = new WebpEncoder
            {
                Quality = quality,
                Method = WebpEncodingMethod.BestQuality, // Slower but better compression
                FileFormat = WebpFileFormatType.Lossy,
                NearLossless = false,
                UseAlphaCompression = true
            };

            await clonedImage.SaveAsWebpAsync(outputPath, encoder);
        }

        public void DeleteImage(ImageSizeUrls? urls)
        {
            if (urls == null) return;

            // Delete all three sizes
            DeleteFileFromUrl(urls.SmallUrl);
            DeleteFileFromUrl(urls.MediumUrl);
            DeleteFileFromUrl(urls.LargeUrl);

            // Try to delete the parent folder if empty
            try
            {
                var firstUrl = urls.SmallUrl ?? urls.MediumUrl ?? urls.LargeUrl;
                if (!string.IsNullOrEmpty(firstUrl))
                {
                    var physicalPath = Path.Combine(_environment.WebRootPath, firstUrl.TrimStart('/'));
                    var directory = Path.GetDirectoryName(physicalPath);
                    if (directory != null && Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        Directory.Delete(directory);
                    }
                }
            }
            catch
            {
                // Fail silently if folder cleanup fails
            }
        }

        private void DeleteFileFromUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return;
            var physicalPath = Path.Combine(_environment.WebRootPath, url.TrimStart('/'));
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }
    }
}

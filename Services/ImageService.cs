using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Night.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

namespace Night.Services
{
    // Handles saving, downloading, resizing (to WebP) and deleting images for the site.
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;

        // Responsive image breakpoints
        private const int WidthTiny = 320;
        private const int WidthSmall = 640;
        private const int WidthMedium = 1024;
        private const int WidthLarge = 1600;
        private const int WidthXLarge = 2400;

        // WebP quality per size
        private const int QualitySmall = 85;
        private const int QualityMedium = 80;
        private const int QualityLarge = 75;

        public ImageService(IWebHostEnvironment environment, HttpClient httpClient)
        {
            _environment = environment;
            _httpClient = httpClient;
        }

        // Builds a short folder name from today's date plus a random 4-char code, e.g. "20260815_a1b2".
        private string GenerateBaseFileName()
        {
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var code = GenerateShortCode();
            return $"{date}_{code}";
        }

        private string GenerateShortCode()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var randomBytes = new byte[4];
            RandomNumberGenerator.Fill(randomBytes);
            var result = new char[4];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = chars[randomBytes[i] % chars.Length];
            }
            return new string(result);
        }

        public async Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type)
        {
            if (file == null || file.Length == 0) return null;

            string subFolder = type switch
            {
                ImageType.Games => "images/games",
                ImageType.Developers => "images/developers",
                ImageType.Events => "images/events",
                ImageType.Members => "images/members",
                ImageType.BlogPosts => "images/blogposts",
                ImageType.Misc => "images/misc",
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
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                if (imageBytes.Length == 0) return null;

                string subFolder = type switch
                {
                    ImageType.Games => "images/games",
                    ImageType.Developers => "images/developers",
                    ImageType.Events => "images/events",
                    ImageType.Members => "images/members",
                    ImageType.BlogPosts => "images/blogposts",
                    ImageType.Misc => "images/misc",
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
                Console.WriteLine($"Error downloading and processing image from URL {imageUrl}: {ex.Message}");
                return null;
            }
        }
        private async Task SaveResizedWebPAsync(Image sourceImage, string outputPath, int targetWidth, int quality)
        {
            // Don't upscale images smaller than the target width - avoids larger, blurrier files on mobile.
            var effectiveWidth = Math.Min(targetWidth, sourceImage.Width);

            using var clonedImage = sourceImage.Clone(ctx =>
            {
                // Auto-rotate based on EXIF orientation - phone cameras commonly store images
                // sideways/upside-down with an orientation tag that must be applied manually.
                ctx.AutoOrient();
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(effectiveWidth, 0),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Lanczos3,
                    Compand = true
                });
            });

            var encoder = new WebpEncoder
            {
                Quality = quality,
                Method = WebpEncodingMethod.BestQuality, 
                FileFormat = WebpFileFormatType.Lossy,
                NearLossless = false,
                UseAlphaCompression = true
            };

            await clonedImage.SaveAsWebpAsync(outputPath, encoder);
        }

        public void DeleteImage(ImageSizeUrls? urls)
        {
            if (urls == null) return;

         
            DeleteFileFromUrl(urls.SmallUrl);
            DeleteFileFromUrl(urls.MediumUrl);
            DeleteFileFromUrl(urls.LargeUrl);

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

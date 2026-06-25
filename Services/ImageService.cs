using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

namespace Night.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;

        private const int WidthSmall = 400;
        private const int WidthMedium = 800;
        private const int WidthLarge = 1200;
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

            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, smallName), WidthSmall);
            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, mediumName), WidthMedium);
            await SaveResizedWebPAsync(image, Path.Combine(imageFolder, largeName), WidthLarge);

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

                // Save resized versions
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, smallName), WidthSmall);
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, mediumName), WidthMedium);
                await SaveResizedWebPAsync(image, Path.Combine(imageFolder, largeName), WidthLarge);

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

        private async Task SaveResizedWebPAsync(Image sourceImage, string outputPath, int targetWidth)
        {
            using var clonedImage = sourceImage.Clone(ctx =>
            {
                ctx.Resize(new ResizeOptions
                {
                    Size = new Size(targetWidth, 0),
                    Mode = ResizeMode.Max
                });
            });

            await clonedImage.SaveAsWebpAsync(outputPath);
        }

        public void DeleteImage(ImageSizeUrls? urls)
        {
            if (urls == null) return;
            DeleteFileFromUrl(urls.SmallUrl);
            DeleteFileFromUrl(urls.MediumUrl);
            DeleteFileFromUrl(urls.LargeUrl);
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

//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.Processing;

//namespace Night.Services
//{
//    public class ImageService : IImageService
//    {
//        private readonly IWebHostEnvironment _environment;

//        private const int WidthSmall = 400;
//        private const int WidthMedium = 800;
//        private const int WidthLarge = 1200;
//        public ImageService(IWebHostEnvironment environment)
//        {
//            _environment = environment;
//        }

//        public async Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type)
//        {
//            if (file == null || file.Length == 0) return null;

           
//            string subFolder = type switch
//            {
//                ImageType.Games => "images/games",
//                ImageType.Developers => "images/developers",
//                ImageType.Events => "images/events",
//                _ => "images/misc"
//            };

//            var targetFolder = Path.Combine(_environment.WebRootPath, subFolder);
//            if (!Directory.Exists(targetFolder))
//            {
//                Directory.CreateDirectory(targetFolder);
//            }

         
//            var baseFileName = Guid.NewGuid().ToString();
//            var smallName = $"{baseFileName}_sm.webp";
//            var mediumName = $"{baseFileName}_md.webp";
//            var largeName = $"{baseFileName}_lg.webp";

          
//            using var stream = file.OpenReadStream();
//            using var image = await Image.LoadAsync(stream);

       
//            await SaveResizedWebPAsync(image, Path.Combine(targetFolder, smallName), WidthSmall);
//            await SaveResizedWebPAsync(image, Path.Combine(targetFolder, mediumName), WidthMedium);
//            await SaveResizedWebPAsync(image, Path.Combine(targetFolder, largeName), WidthLarge);

//            return new ImageSizeUrls(
//                SmallUrl: $"/{subFolder}/{smallName}",
//                MediumUrl: $"/{subFolder}/{mediumName}",
//                LargeUrl: $"/{subFolder}/{largeName}"
//            );
//        }

//        private async Task SaveResizedWebPAsync(Image sourceImage, string outputPath, int targetWidth)
//        {
        
//            using var clonedImage = sourceImage.Clone(ctx =>
//            {
//                if (sourceImage.Width > targetWidth)
//                {
//                    ctx.Resize(new ResizeOptions
//                    {
                   
//                        Size = new Size(targetWidth, 0),
//                        Mode = ResizeMode.Max
//                    });
//                }
//            });

 
//            await clonedImage.SaveAsWebpAsync(outputPath);
//        }

//        public void DeleteImage(ImageSizeUrls? urls)
//        {
//            if (urls == null) return;
//            DeleteFileFromUrl(urls.SmallUrl);
//            DeleteFileFromUrl(urls.MediumUrl);
//            DeleteFileFromUrl(urls.LargeUrl);
//        }

//        private void DeleteFileFromUrl(string? url)
//        {
//            if (string.IsNullOrEmpty(url)) return;
//            var physicalPath = Path.Combine(_environment.WebRootPath, url.TrimStart('/'));
//            if (File.Exists(physicalPath))
//            {
//                File.Delete(physicalPath);
//            }
//        }
//    }
//}

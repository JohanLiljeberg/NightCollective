using Night.Models;

namespace Night.Services
{
    public record ImageSizeUrls(string SmallUrl, string MediumUrl, string LargeUrl);

    public interface IImageService
    { 
        Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type);
        Task<ImageSizeUrls?> DownloadAndProcessUrlAsync(string imageUrl, ImageType type);
        void DeleteImage(ImageSizeUrls relativeUrl);
    }
}

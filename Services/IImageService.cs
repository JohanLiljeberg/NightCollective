namespace Night.Services
{

    public enum ImageType
    {
    Games,
    Developers,
    Events,
    BlogPosts
    }

    public record ImageSizeUrls(string SmallUrl, string MediumUrl, string LargeUrl);
    public interface IImageService
    { 
        Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type);
        Task<ImageSizeUrls?> DownloadAndProcessUrlAsync(string imageUrl, ImageType type);
        void DeleteImage(ImageSizeUrls relativeUrl);
    }
}

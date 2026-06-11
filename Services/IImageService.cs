namespace Night.Services
{

    public enum ImageType
    {
    Games,
    Developers,
    Events
    }

    public record ImageSizeUrls(string SmallUrl, string MediumUrl, string LargeUrl);
    public interface IImageService
    { 
        Task<ImageSizeUrls?> UploadImageAsync(IFormFile? file, ImageType type);
        void DeleteImage(ImageSizeUrls relativeUrl);
    }
}

using Microsoft.AspNetCore.Mvc;
using Night.Models;

namespace Night.ViewComponents;

public class ResponsiveImageVC : ViewComponent
{
    public IViewComponentResult Invoke(
        string? smallUrl,
        string? mediumUrl,
        string? largeUrl,
        string altText,
        string cssClass = "img-fluid",
        int? width = null,
        int? height = null,
        string? aspectRatio = null)
    {
        var model = new ResponsiveImageModel
        {
            ImageSmallUrl = smallUrl,
            ImageMediumUrl = mediumUrl,
            ImageLargeUrl = largeUrl,
            Image = smallUrl ?? mediumUrl ?? largeUrl ?? string.Empty,
            AltText = altText,
            CssClass = cssClass,
            Width = width,
            Height = height,
            AspectRatio = aspectRatio
        };

        return View(model);
    }
}

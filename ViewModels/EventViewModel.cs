using System.ComponentModel.DataAnnotations;

namespace Night.ViewModels;

public class EventViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public DateTime Date { get; init; }

    public string DateDisplay => Date.ToString("MMMM d, yyyy");

    public string Location { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    // Responsive image URLs
    public string ImageSmallUrl { get; init; } = string.Empty;
    public string ImageMediumUrl { get; init; } = string.Empty;
    public string ImageLargeUrl { get; init; } = string.Empty;

    // Backwards compatible single URL (medium by default)
    public string ImageUrl => ImageMediumUrl ?? ImageSmallUrl ?? ImageLargeUrl ?? string.Empty;
}

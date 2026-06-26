public record ResponsiveImageModel
{
    public string? ImageSmallUrl { get; init; }
    public string? ImageMediumUrl { get; init; }
    public string? ImageLargeUrl { get; init; }
    public string Image { get; init; } = string.Empty; // fallback
    public string AltText { get; init; } = string.Empty;
}
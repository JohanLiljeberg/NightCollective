namespace Night.ViewModels;

public class GameScreenshotViewModel
{
    public int Id { get; init; }

    public string? ImageSmallUrl { get; init; }
    public string? ImageMediumUrl { get; init; }
    public string? ImageLargeUrl { get; init; }

    public int DisplayOrder { get; init; }
}

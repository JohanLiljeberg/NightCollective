namespace Night.Models;

public class GameScreenshot
{
    public int Id { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public string? ImageSmallUrl { get; set; }
    public string? ImageMediumUrl { get; set; }
    public string? ImageLargeUrl { get; set; }

    public int DisplayOrder { get; set; } // For ordering screenshots (1, 2, 3)
}

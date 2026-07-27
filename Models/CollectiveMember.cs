namespace Night.Models;

public class CollectiveMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;

    // Legacy + Responsive Images
    public string Image { get; set; } = string.Empty;
    public string? ImageSmallUrl { get; set; }
    public string? ImageMediumUrl { get; set; }
    public string? ImageLargeUrl { get; set; }

    // Navigation
    public List<Game> Games { get; set; } = new();

    public List<GameMemberContribution> GameContributions { get; set; } = new();
}
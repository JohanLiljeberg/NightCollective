namespace Night.Models;

public class CollectiveMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;

    // Membership
    public MembershipType MembershipType { get; set; } = MembershipType.Full;

    // For Subscribed members - their featured game
    public int? FeaturedGameId { get; set; }
    public Game? FeaturedGame { get; set; }

    // Legacy + Responsive Images
    public string Image { get; set; } = string.Empty;
    public string? ImageSmallUrl { get; set; }
    public string? ImageMediumUrl { get; set; }
    public string? ImageLargeUrl { get; set; }

    // Navigation
    public List<Game> Games { get; set; } = new();

    public List<GameMemberContribution> GameContributions { get; set; } = new();
}
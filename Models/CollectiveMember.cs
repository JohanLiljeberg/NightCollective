namespace Night.Models;

public class CollectiveMember
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Position { get; set; }
    public string? Quote { get; set; }

    // Membership
    public MembershipType MembershipType { get; set; } = MembershipType.Full;

    // Allows an admin to hide this member from public pages without deleting them
    public bool IsHidden { get; set; }

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
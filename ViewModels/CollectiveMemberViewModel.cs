using Night.Models;

namespace Night.ViewModels;

public class CollectiveMemberViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Image { get; init; } = string.Empty;

    public string? ImageSmallUrl { get; init; }

    public string? ImageMediumUrl { get; init; }

    public string? ImageLargeUrl { get; init; }

    public string Position { get; init; } = string.Empty;

    public string Quote { get; init; } = string.Empty;

    public MembershipType MembershipType { get; init; } = MembershipType.Full;

    // For Subscribed members
    public GameViewModel? FeaturedGame { get; init; }

    public IReadOnlyCollection<GameViewModel> Games { get; init; } = [];

    public IReadOnlyCollection<GameContributionViewModel> GameContributions { get; init; } = [];
}

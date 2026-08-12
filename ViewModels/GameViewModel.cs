using Night.Models;

namespace Night.ViewModels;

public class GameViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public int ReleaseYear { get; init; }

    public string Image { get; init; } = string.Empty;

    public string? ImageSmallUrl { get; init; }

    public string? ImageMediumUrl { get; init; }

    public string? ImageLargeUrl { get; init; }

    public string DeveloperPublisher { get; init; } = string.Empty;

    public Platforms Platforms { get; init; } = Platforms.PC;

    public genreGameplayType GenreGameplayType { get; init; } = genreGameplayType.Action;

    public bool FromCollective { get; init; }

    public string Description { get; init; } = string.Empty;

    public string? YouTubeTrailerUrl { get; init; }

    public IReadOnlyCollection<GameScreenshotViewModel> Screenshots { get; init; } = [];

    public IReadOnlyCollection<string> MemberNames { get; init; } = [];

    public IReadOnlyCollection<MemberContributionViewModel> MemberContributions { get; init; } = [];
}

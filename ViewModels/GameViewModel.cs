namespace Night.ViewModels;

public class GameViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public int ReleaseYear { get; init; }

    public string Image { get; init; } = string.Empty;

    public string DeveloperPublisher { get; init; } = string.Empty;

    public string Platforms { get; init; } = string.Empty;

    public string GenreGameplayType { get; init; } = string.Empty;

    public bool FromCollective { get; init; }

    public int? CollectiveMemberId { get; init; }

    public string CollectiveMemberName { get; init; } = string.Empty;
}

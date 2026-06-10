namespace Night.ViewModels;

public class CollectiveMemberViewModel
{
    public string Name { get; init; } = string.Empty;

    public string Image { get; init; } = string.Empty;

    public string Position { get; init; } = string.Empty;

    public string Quote { get; init; } = string.Empty;

    public IReadOnlyCollection<GameViewModel> Games { get; init; } = [];
}

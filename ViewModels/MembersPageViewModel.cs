namespace Night.ViewModels;

public class MembersPageViewModel
{
    public IReadOnlyCollection<CollectiveMemberViewModel> Members { get; init; } = [];

    public IReadOnlyCollection<GameViewModel> Games { get; init; } = [];
}

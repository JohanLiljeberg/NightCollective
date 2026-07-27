using Microsoft.AspNetCore.Mvc.Rendering;

namespace Night.ViewModels;

public class MembersPageViewModel
{
    public IReadOnlyCollection<CollectiveMemberViewModel> Members { get; init; } = [];

    public IReadOnlyCollection<GameViewModel> Games { get; init; } = [];

    public CollectiveMemberFormViewModel MemberForm { get; init; } = new();

    public GameFormViewModel GameForm { get; init; } = new();

    public IReadOnlyCollection<SelectListItem> MemberOptions { get; init; } = [];
}

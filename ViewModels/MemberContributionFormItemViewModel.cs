using Microsoft.AspNetCore.Mvc.Rendering;

namespace Night.ViewModels;

public class MemberContributionFormItemViewModel
{
    public GameMemberContributionFormViewModel Contribution { get; set; } = new();
    public List<MemberSelectViewModel> AvailableMembers { get; set; } = new();
    public int Index { get; set; }
    public string GameFormId { get; set; } = string.Empty;
}

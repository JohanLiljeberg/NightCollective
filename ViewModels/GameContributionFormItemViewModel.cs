using Microsoft.AspNetCore.Mvc.Rendering;

namespace Night.ViewModels;

public class GameContributionFormItemViewModel
{
    public MemberGameContributionFormViewModel Contribution { get; set; } = new();
    public List<SelectListItem> AvailableGames { get; set; } = new();
    public int Index { get; set; }
    public string MemberFormId { get; set; } = string.Empty;
}

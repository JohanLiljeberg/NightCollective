using Night.Models;

namespace Night.ViewModels;

public class MemberGameContributionFormViewModel
{
    public int GameId { get; set; }
    public InvolvementLevel InvolvementLevel { get; set; } = InvolvementLevel.Supporting;
    public List<WorkArea> SelectedWorkAreas { get; set; } = new();
}

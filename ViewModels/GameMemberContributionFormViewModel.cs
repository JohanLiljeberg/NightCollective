using Night.Models;

namespace Night.ViewModels;

public class GameMemberContributionFormViewModel
{
    public int MemberId { get; set; }
    public InvolvementLevel InvolvementLevel { get; set; } = InvolvementLevel.Supporting;
    public List<WorkArea> SelectedWorkAreas { get; set; } = new();
}

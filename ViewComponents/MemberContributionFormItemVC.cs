using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Night.ViewModels;

namespace Night.ViewComponents;

public class MemberContributionFormItemVC : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(
        GameMemberContributionFormViewModel contribution,
        List<SelectListItem> availableMembers,
        int index,
        string gameFormId)
    {
        var viewModel = new MemberContributionFormItemViewModel
        {
            Contribution = contribution,
            AvailableMembers = availableMembers,
            Index = index,
            GameFormId = gameFormId
        };

        return Task.FromResult<IViewComponentResult>(View(viewModel));
    }
}

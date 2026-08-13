using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Night.ViewModels;

namespace Night.ViewComponents;

public class GameContributionFormItemVC : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(
        MemberGameContributionFormViewModel contribution,
        List<SelectListItem> availableGames,
        int index,
        string memberFormId)
    {
        var viewModel = new GameContributionFormItemViewModel
        {
            Contribution = contribution,
            AvailableGames = availableGames,
            Index = index,
            MemberFormId = memberFormId
        };

        return Task.FromResult<IViewComponentResult>(View(viewModel));
    }
}

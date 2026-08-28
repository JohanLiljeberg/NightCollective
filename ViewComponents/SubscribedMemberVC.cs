using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class SubscribedMemberVC : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync(CollectiveMemberViewModel member)
    {
        return Task.FromResult<IViewComponentResult>(View(member));
    }
}

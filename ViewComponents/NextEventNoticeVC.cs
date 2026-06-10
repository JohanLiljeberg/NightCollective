using Microsoft.AspNetCore.Mvc;
using Night.Services;

namespace Night.ViewComponents;

public class NextEventNoticeVC(ICollectiveService collectiveService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var collectiveEvent = await collectiveService.GetNextUpcomingEventBasicInfoAsync();

        return View(collectiveEvent);
    }
}

using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class EventBasicInfoVC : ViewComponent
{
    public IViewComponentResult Invoke(EventBasicInfoViewModel collectiveEvent)
    {
        return View(collectiveEvent);
    }
}

using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class EventCardVC : ViewComponent
{
    public IViewComponentResult Invoke(EventViewModel collectiveEvent)
    {
        return View(collectiveEvent);
    }
}

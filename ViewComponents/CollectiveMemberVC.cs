using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class CollectiveMemberVC : ViewComponent
{
    public IViewComponentResult Invoke(CollectiveMemberViewModel member)
    {
        return View(member);
    }
}

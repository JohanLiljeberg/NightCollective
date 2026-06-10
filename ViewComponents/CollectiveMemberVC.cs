using Microsoft.AspNetCore.Mvc;
using Night.Models;

namespace Night.ViewComponents;

public class CollectiveMemberVC : ViewComponent
{
    public IViewComponentResult Invoke(CollectiveMember member)
    {
        return View(member);
    }
}

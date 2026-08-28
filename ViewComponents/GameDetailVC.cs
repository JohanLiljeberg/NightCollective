using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class GameDetailVC : ViewComponent
{
    public IViewComponentResult Invoke(GameViewModel game)
    {
        return View(game);
    }
}

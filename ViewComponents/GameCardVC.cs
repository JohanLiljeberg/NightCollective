using Microsoft.AspNetCore.Mvc;
using Night.ViewModels;

namespace Night.ViewComponents;

public class GameCardVC : ViewComponent
{
    public IViewComponentResult Invoke(GameViewModel game)
    {
        return View(game);
    }
}

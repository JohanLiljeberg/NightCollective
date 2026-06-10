using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Night.Models;
using Night.Services;

namespace Night.Controllers;

public class HomeController(ICollectiveService collectiveService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = await collectiveService.GetHomePageContentAsync();

        return View(viewModel);
    }

    public async Task<IActionResult> Members()
    {
        var members = await collectiveService.GetCollectiveMembersAsync();

        return View(members);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

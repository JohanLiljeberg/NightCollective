using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Night.Models;
using Night.Services;

namespace Night.Controllers;

public class HomeController(ICollectiveService collectiveService) : Controller
{
    public IActionResult Index()
    {
        var viewModel = collectiveService.GetHomePageContent();

        return View(viewModel);
    }

    public IActionResult Members()
    {
        var members = collectiveService.GetCollectiveMembers();

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

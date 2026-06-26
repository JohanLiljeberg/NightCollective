using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Night.Models;
using Night.Services;
using Night.ViewModels;

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
        var viewModel = await collectiveService.GetMembersPageAsync();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGame([Bind(Prefix = "GameForm")] GameFormViewModel gameForm)
    {
        if (!ModelState.IsValid)
        {
            return View("Members", await collectiveService.GetMembersPageAsync());
        }

        await collectiveService.AddGameAsync(gameForm);

        return RedirectToAction(nameof(Members));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMember([Bind(Prefix = "MemberForm")] CollectiveMemberFormViewModel memberForm)
    {
        if (!ModelState.IsValid)
        {
            return View("Members", await collectiveService.GetMembersPageAsync());
        }

        await collectiveService.AddCollectiveMemberAsync(memberForm);

        return RedirectToAction(nameof(Members));
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

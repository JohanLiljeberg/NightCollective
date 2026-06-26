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
    public async Task<IActionResult> AddMember([Bind(Prefix = "MemberForm")] CollectiveMemberFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Members));
        }

        await collectiveService.AddCollectiveMemberAsync(viewModel);
        return RedirectToAction(nameof(Members));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMember([Bind(Prefix = "MemberForm")] CollectiveMemberFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Members));
        }

        await collectiveService.UpdateCollectiveMemberAsync(viewModel);
        return RedirectToAction(nameof(Members));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGame([Bind(Prefix = "GameForm")] GameFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Members));
        }

        await collectiveService.AddGameAsync(viewModel);
        return RedirectToAction(nameof(Members));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGame([Bind(Prefix = "GameForm")] GameFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Members));
        }

        await collectiveService.UpdateGameAsync(viewModel);
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

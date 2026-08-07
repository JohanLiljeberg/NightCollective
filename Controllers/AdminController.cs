using Microsoft.AspNetCore.Mvc;
using Night.Filters;
using Night.Services;
using Night.ViewModels;

namespace Night.Controllers;

public class AdminController(
    IConfiguration configuration,
    IEventService eventService,
    ICollectiveService collectiveService,
    IBlogPostService blogPostService) : Controller
{
    public IActionResult Login()
    {
        // If already logged in, redirect to dashboard
        if (HttpContext.Session.GetString("IsAdmin") == "true")
        {
            return RedirectToAction(nameof(Dashboard));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string password)
    {
        var adminPassword = configuration["Admin:Password"];

        if (password == adminPassword)
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction(nameof(Dashboard));
        }

        ViewBag.Error = "Invalid password";
        return View();
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public async Task<IActionResult> Dashboard()
    {
        var viewModel = new AdminDashboardViewModel
        {
            EventForm = await eventService.GetCreateEventFormAsync(),
            GameForm = await collectiveService.GetGameFormAsync(),
            MemberForm = await collectiveService.GetMemberFormAsync(),
            BlogPostForm = await blogPostService.GetCreateBlogPostFormAsync(),
            AllMembers = await collectiveService.GetCollectiveMembersAsync(),
            AllGames = await collectiveService.GetGamesAsync(),
            AllBlogPosts = await blogPostService.GetAllBlogPostsAsync()
        };

        return View(viewModel);
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEvent(EventFormViewModel eventForm)
    {
        bool hasFile = eventForm.ImageFile is not null && eventForm.ImageFile.Length > 0;
        bool hasUrl = !string.IsNullOrWhiteSpace(eventForm.ImageUrl?.Trim());

        if (!hasFile && !hasUrl)
        {
            ModelState.AddModelError("", "Please provide an image by uploading a file or entering a URL.");
        }

        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = eventForm,
                GameForm = await collectiveService.GetGameFormAsync(),
                MemberForm = await collectiveService.GetMemberFormAsync()
            };
            return View("Dashboard", viewModel);
        }

        await eventService.CreateEventAsync(eventForm);
        TempData["SuccessMessage"] = "Event created successfully!";
        return RedirectToAction(nameof(Dashboard));
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateGame([Bind(Prefix = "GameForm")] GameFormViewModel gameForm)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = gameForm,
                MemberForm = await collectiveService.GetMemberFormAsync()
            };
            return View("Dashboard", viewModel);
        }

        await collectiveService.AddGameAsync(gameForm);
        TempData["SuccessMessage"] = "Game added successfully!";
        return RedirectToAction(nameof(Dashboard));
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMember([Bind(Prefix = "MemberForm")] CollectiveMemberFormViewModel memberForm)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = await collectiveService.GetGameFormAsync(),
                MemberForm = memberForm
            };
            return View("Dashboard", viewModel);
        }

        await collectiveService.AddCollectiveMemberAsync(memberForm);
        TempData["SuccessMessage"] = "Member added successfully!";
        return RedirectToAction(nameof(Dashboard));
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateMember([Bind(Prefix = "MemberForm")] CollectiveMemberFormViewModel memberForm)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = await collectiveService.GetGameFormAsync(),
                MemberForm = memberForm,
                AllMembers = await collectiveService.GetCollectiveMembersAsync()
            };
            return View("Dashboard", viewModel);
        }

        await collectiveService.UpdateCollectiveMemberAsync(memberForm);
        TempData["SuccessMessage"] = "Member updated successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-members" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMember(int id)
    {
        await collectiveService.DeleteCollectiveMemberAsync(id);
        TempData["SuccessMessage"] = "Member deleted successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-members" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateGame([Bind(Prefix = "GameForm")] GameFormViewModel gameForm)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = gameForm,
                MemberForm = await collectiveService.GetMemberFormAsync(),
                AllMembers = await collectiveService.GetCollectiveMembersAsync(),
                AllGames = await collectiveService.GetGamesAsync()
            };
            return View("Dashboard", viewModel);
        }

        await collectiveService.UpdateGameAsync(gameForm);
        TempData["SuccessMessage"] = "Game updated successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-games" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteGame(int id)
    {
        await collectiveService.DeleteGameAsync(id);
        TempData["SuccessMessage"] = "Game deleted successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-games" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBlogPost([Bind(Prefix = "BlogPostForm")] BlogPostFormViewModel blogPostForm)
    {
        bool hasFile = blogPostForm.ImageFile is not null && blogPostForm.ImageFile.Length > 0;
        bool hasUrl = !string.IsNullOrWhiteSpace(blogPostForm.ImageUrl?.Trim());

        if (!hasFile && !hasUrl)
        {
            ModelState.AddModelError("", "Please provide an image by uploading a file or entering a URL.");
        }

        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = await collectiveService.GetGameFormAsync(),
                MemberForm = await collectiveService.GetMemberFormAsync(),
                BlogPostForm = blogPostForm,
                AllBlogPosts = await blogPostService.GetAllBlogPostsAsync()
            };
            return View("Dashboard", viewModel);
        }

        await blogPostService.CreateBlogPostAsync(blogPostForm);
        TempData["SuccessMessage"] = "Blog post created successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "blog-posts" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBlogPost([Bind(Prefix = "BlogPostForm")] BlogPostFormViewModel blogPostForm)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new AdminDashboardViewModel
            {
                EventForm = await eventService.GetCreateEventFormAsync(),
                GameForm = await collectiveService.GetGameFormAsync(),
                MemberForm = await collectiveService.GetMemberFormAsync(),
                BlogPostForm = blogPostForm,
                AllBlogPosts = await blogPostService.GetAllBlogPostsAsync()
            };
            return View("Dashboard", viewModel);
        }

        await blogPostService.UpdateBlogPostAsync(blogPostForm);
        TempData["SuccessMessage"] = "Blog post updated successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-blog-posts" });
    }

    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        await blogPostService.DeleteBlogPostAsync(id);
        TempData["SuccessMessage"] = "Blog post deleted successfully!";
        return RedirectToAction(nameof(Dashboard), new { tab = "manage-blog-posts" });
    }
}

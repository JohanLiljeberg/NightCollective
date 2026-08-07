using Microsoft.AspNetCore.Mvc;
using Night.Services;

namespace Night.Controllers;

public class BlogController(IBlogPostService blogPostService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var blogPosts = await blogPostService.GetAllBlogPostsAsync();
        var publishedPosts = blogPosts.Where(p => p.IsPublished).ToList();

        return View(publishedPosts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var blogPost = await blogPostService.GetBlogPostAsync(id);

        if (blogPost is null || !blogPost.IsPublished)
        {
            return NotFound();
        }

        return View(blogPost);
    }
}

using Microsoft.AspNetCore.Mvc;
using Night.Models;

namespace Night.ViewComponents;

public class BlogPostCardVC : ViewComponent
{
    public IViewComponentResult Invoke(BlogPost blogPost)
    {
        return View(blogPost);
    }
}

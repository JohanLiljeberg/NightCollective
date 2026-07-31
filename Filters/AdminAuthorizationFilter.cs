using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Night.Filters;

public class AdminAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAdmin = context.HttpContext.Session.GetString("IsAdmin");

        if (isAdmin != "true")
        {
            context.Result = new RedirectToActionResult("Login", "Admin", null);
        }
    }
}

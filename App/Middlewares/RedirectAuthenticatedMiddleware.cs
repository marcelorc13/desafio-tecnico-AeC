using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Middlewares;

public class RedirectAuthenticatedMiddleware(RequestDelegate next)
{
    private static readonly string[] GuestOnlyPaths = ["/auth/login", "/auth/register"];

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true &&
            GuestOnlyPaths.Contains(context.Request.Path.Value?.ToLower()))
        {
            var result = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded || result.Properties?.ExpiresUtc <= DateTimeOffset.UtcNow)
            {
                await next(context);
                return;
            }

            context.Response.Redirect("/addresses");
            return;
        }

        await next(context);
    }
}

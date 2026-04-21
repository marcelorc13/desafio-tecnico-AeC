namespace App.Middlewares;

public class RedirectAuthenticatedMiddleware(RequestDelegate next)
{
    private static readonly string[] GuestOnlyPaths = ["/auth/login", "/auth/register"];

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true &&
            GuestOnlyPaths.Contains(context.Request.Path.Value?.ToLower()))
        {
            context.Response.Redirect("/addresses");
            return;
        }

        await next(context);
    }
}

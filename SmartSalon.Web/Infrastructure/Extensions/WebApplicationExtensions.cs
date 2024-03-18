
namespace SmartSalon.Web.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void OpenSwaggerOnStartup(this WebApplication app)
        => app.MapGet("/", (context) =>
        {
            context.Response.Redirect("swagger/index.html");
            return Task.CompletedTask;
        });
}
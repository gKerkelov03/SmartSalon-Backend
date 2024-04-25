using Microsoft.EntityFrameworkCore;
using SmartSalon.Data;

namespace SmartSalon.Presentation.Web.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MigrateTheDatabase(this WebApplication app, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SmartSalonDbContext>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
        }

        return app;
    }

    public static WebApplication OpenSwaggerOnStartup(this WebApplication app)
    {
        app.MapGet("/", (context) =>
        {
            context.Response.Redirect("swagger/index.html");
            return Task.CompletedTask;
        });

        return app;
    }
}
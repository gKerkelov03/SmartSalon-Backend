using Microsoft.EntityFrameworkCore;
using SmartSalon.Data;
using SmartSalon.Data.Seeding;

namespace SmartSalon.Presentation.Web.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication SeedTheDatabase(this WebApplication app, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var seeder = serviceProvider.GetRequiredService<ISeeder>();
        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<SmartSalonDbContext>();

        // dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        // seeder
        //     .SeedAsync(dbContext, scope.ServiceProvider)
        //     .GetAwaiter()
        //     .GetResult();

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
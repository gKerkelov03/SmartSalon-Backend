using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Data;
using SmartSalon.Data.Seeding;

namespace SmartSalon.Web.Infrastructure.Extensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder AddSeeding(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<SmartSalonDbContext>();

        var seeder = serviceProvider.GetRequiredService<ISeeder>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        seeder
            .SeedAsync(dbContext, scope.ServiceProvider)
            .GetAwaiter()
            .GetResult();

        return app;
    }

    public static IApplicationBuilder AddSwaggerUI(this IApplicationBuilder app, IWebHostEnvironment environment, IServiceProvider serviceProvider)
    {
        if (!environment.IsDevelopment())
        {
            return app;
        }

        return app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                var apiVersionsInfo = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var versionDescription in apiVersionsInfo.ApiVersionDescriptions)
                {
                    var url = $"/swagger/{versionDescription.GroupName}/swagger.json";
                    var name = versionDescription.ApiVersion.ToString();

                    options.SwaggerEndpoint(url, name);
                }
            });
    }
}

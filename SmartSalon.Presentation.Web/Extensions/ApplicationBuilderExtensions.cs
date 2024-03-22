using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SmartSalon.Presentation.Web.Extensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        return app.UseExceptionHandler("/Error");
    }

    public static IApplicationBuilder AddSwaggerUI(
        this IApplicationBuilder app,
        IWebHostEnvironment environment,
        IServiceProvider serviceProvider)
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

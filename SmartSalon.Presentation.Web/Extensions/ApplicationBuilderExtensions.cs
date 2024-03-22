using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

namespace SmartSalon.Presentation.Web.Extensions;

public static class IApplicationBuilderExtensions
{
    //TODO: ask about the dev exception page
    public static IApplicationBuilder AddExceptionHandling(this IApplicationBuilder app)
        => app
            .UseDeveloperExceptionPage()
            .UseExceptionHandler("/Error");

    public static IApplicationBuilder AddLogging(this IApplicationBuilder app)
    {
        app
            .UseSerilogRequestLogging();

        return app;
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

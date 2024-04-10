using Asp.Versioning;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class ApiVersioningOptionsConfigurator : IConfigureOptions<ApiVersioningOptions>, ISingletonLifetime
{
    public void Configure(ApiVersioningOptions options)
    {
        options.DefaultApiVersion = new(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
}

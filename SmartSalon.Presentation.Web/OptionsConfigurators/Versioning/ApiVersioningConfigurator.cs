using Asp.Versioning;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.OptionsConfigurators.Versioning;

public class ApiVersioningConfigurator : IConfigureOptions<ApiVersioningOptions>, ITransientLifetime
{
    public void Configure(ApiVersioningOptions options)
    {
        options.DefaultApiVersion = new(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
}

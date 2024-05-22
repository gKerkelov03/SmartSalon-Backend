using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.OptionsConfigurators.Versioning;

public class ApiExplorerConfigurator : IConfigureOptions<ApiExplorerOptions>, ITransientLifetime
{
    public void Configure(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'V'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}

using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class ApiExplorerOptionsConfigurator : IConfigureOptions<ApiExplorerOptions>, ISingletonLifetime
{
    public void Configure(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'V'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}

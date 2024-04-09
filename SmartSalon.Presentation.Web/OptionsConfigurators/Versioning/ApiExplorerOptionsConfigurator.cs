using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class ApiExplorerOptionsConfigurator : IConfigureOptions<ApiExplorerOptions>
{
    public void Configure(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'V'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class ApiExplorerOptionsConfigurator : IConfigureOptions<ApiExplorerOptions>
{
    public void Configure(ApiExplorerOptions options)
    {
        //VVV means major.minor.patch => v1.2.3
        options.GroupNameFormat = "'V'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.Options;

public class SwaggerGenOptionsConfigurator : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionsInfo;

    public SwaggerGenOptionsConfigurator(IApiVersionDescriptionProvider provider)
        => _apiVersionsInfo = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var versionDescription in _apiVersionsInfo.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                versionDescription.GroupName,
                GenerateVersionInfo(versionDescription)
            );
        }
    }

    private OpenApiInfo GenerateVersionInfo(ApiVersionDescription versionDescription)
    {
        var info = new OpenApiInfo()
        {
            Title = SystemName,
            Version = versionDescription.ApiVersion.ToString()
        };

        if (versionDescription.IsDeprecated)
        {
            info.Description = "This API version is deprecated";
        }

        return info;
    }
}

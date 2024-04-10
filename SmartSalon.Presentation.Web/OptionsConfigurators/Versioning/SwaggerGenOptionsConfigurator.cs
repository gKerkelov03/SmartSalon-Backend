using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SmartSalon.Application.Abstractions.Lifetime;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class SwaggerGenOptionsConfigurator : IConfigureOptions<SwaggerGenOptions>, ISingletonLifetime
{
    private readonly IApiVersionDescriptionProvider _apiVersionsInfo;

    public SwaggerGenOptionsConfigurator(IApiVersionDescriptionProvider provider)
        => _apiVersionsInfo = provider;

    public void Configure(SwaggerGenOptions options)
    {
        var schemeName = "Bearer";
        var scheme = new OpenApiSecurityScheme()
        {
            Reference = new()
            {
                Id = schemeName,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.OperationFilter<HideIdParametersFilter>();
        // options.OperationFilter<ShowUuidInsteadOfStringForIdsFilter>();

        options.AddSecurityDefinition(schemeName, new()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = schemeName,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = $"""
                JWT Authorization header using the Bearer scheme.
                Enter 'Bearer' [space] and then your token 

                Example: '{schemeName} 1a2b3c4d5e6f7g'
            """,
        });

        options.AddSecurityRequirement(new()
        {
            [scheme] = []
        });

        foreach (var versionDescription in _apiVersionsInfo.ApiVersionDescriptions)
        {
            options.SwaggerDoc(versionDescription.GroupName, GenerateVersionInfo(versionDescription));
        }
    }

    private OpenApiInfo GenerateVersionInfo(ApiVersionDescription versionDescription)
    {
        var info = new OpenApiInfo()
        {
            Title = $"SmartSalon {versionDescription.GroupName}",
            Version = versionDescription.ApiVersion.ToString()
        };

        if (versionDescription.IsDeprecated)
        {
            info.Description = "This API version is deprecated";
        }

        return info;
    }
}

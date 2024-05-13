using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartSalon.Presentation.Web.OptionsConfigurators.Versioning;

public class SwaggerGenConfigurator : IConfigureOptions<SwaggerGenOptions>, ITransientLifetime
{
    private readonly IApiVersionDescriptionProvider _apiVersionsInfo;

    public SwaggerGenConfigurator(IApiVersionDescriptionProvider provider)
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
            [scheme] = Array.Empty<string>()
        });

        foreach (var versionDescription in _apiVersionsInfo.ApiVersionDescriptions)
        {
            options.SwaggerDoc(versionDescription.GroupName, GenerateVersionInfo(versionDescription));
        }

        RegisterAllFilters(options);
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

    private void RegisterAllFilters(SwaggerGenOptions options)
    {
        var allTypes = typeof(WebConstants).Assembly.GetTypes();

        var operationFilterTypes = allTypes
            .Where(type => typeof(IOperationFilter).IsAssignableFrom(type) && type.IsNotAbsctractOrInterface());

        var schemaFilterTypes = allTypes
            .Where(type => typeof(ISchemaFilter).IsAssignableFrom(type) && type.IsNotAbsctractOrInterface());

        var operationFiltersRegistrationMethod = typeof(SwaggerGenOptionsExtensions)
            .GetMethod(nameof(SwaggerGenOptionsExtensions.OperationFilter))!;

        var schemaFiltersRegistrationMethod = typeof(SwaggerGenOptionsExtensions)
            .GetMethod(nameof(SwaggerGenOptionsExtensions.SchemaFilter))!;

        foreach (var operationFilter in operationFilterTypes)
        {
            var genericMethod = operationFiltersRegistrationMethod.MakeGenericMethod(operationFilter);
            genericMethod.Invoke(null, [options, Array.Empty<object>()]);
        }

        foreach (var schemaFilter in schemaFilterTypes)
        {
            var genericMethod = schemaFiltersRegistrationMethod.MakeGenericMethod(schemaFilter);
            genericMethod.Invoke(null, [options, Array.Empty<object>()]);
        }
    }
}

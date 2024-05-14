using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using SmartSalon.Application.Options;
using Microsoft.IdentityModel.Tokens;

namespace SmartSalon.Presentation.Web.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAllOptionsClasses(
        this IServiceCollection services,
        IConfiguration config,
        params Assembly[] assemblies
    )
    {
        var optionsTypes = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type.Name.EndsWith("Options") && type.IsNotAbsctractOrInterface());

        var optionsExtensionType = typeof(OptionsConfigurationServiceCollectionExtensions);
        var configureMethod = optionsExtensionType.GetMethods()
            .Where(m => m.Name == "Configure" && m.IsGenericMethod && m.GetParameters().Length == 2)
            .FirstOrDefault();

        foreach (var optionType in optionsTypes)
        {
            var configure = configureMethod!.MakeGenericMethod(optionType);
            var sectionField = optionType.GetField("SectionName", BindingFlags.Static | BindingFlags.Public);

            if (sectionField is null)
            {
                throw new InvalidOperationException("Every Options class needs to end with Options and to have a public static not empty field SectionName");
            }

            var sectionName = sectionField.GetValue(null)!.CastTo<string>();
            configure.Invoke(null, [services, config.GetSection(sectionName)]);
        }

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //TODO: why extracting this options configuration in optionsConfigurator class doesn't get called
                var jwtOptions = config.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
                var signingKey = Encoding.ASCII.GetBytes(jwtOptions.EncryptionKey);

                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection CallAddControllers(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning()
            .AddApiExplorer();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(types =>
        {
            var allTypes = types.FromAssemblies(assemblies);

            allTypes
                .AddClasses(@class => @class.AssignableTo(typeof(IScopedLifetime)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            allTypes
                .AddClasses(@class => @class.AssignableTo(typeof(ISingletonLifetime)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime();

            allTypes
                .AddClasses(@class => @class.AssignableTo(typeof(ITransientLifetime)))
                .AsImplementedInterfaces()
                .WithTransientLifetime();
        });

        return services.AddSingleton<JwtSecurityTokenHandler>();
    }
}

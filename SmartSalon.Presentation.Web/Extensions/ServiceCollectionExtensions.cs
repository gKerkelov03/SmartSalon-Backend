using TypeAndInterfacePair = (System.Type Type, System.Type Interface);
using SourceAndDestinationPair = (System.Type Source, System.Type Destination);
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Extensions;
using SmartSalon.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

    public static IServiceCollection RegisterMapper(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddAutoMapper(expression =>
        {
            var allTypesFromTheAssemblies = assemblies.SelectMany(assembly => assembly.GetExportedTypes());

            expression.CreateProfile(
                "ReflectionProfile",
                options =>
                {
                    GetFromMapsFrom(allTypesFromTheAssemblies)
                        .ForEach(map => options.CreateMap(map.Source, map.Destination));

                    GetToMapsFrom(allTypesFromTheAssemblies)
                        .ForEach(map => options.CreateMap(map.Source, map.Destination));

                    GetCustomMappingsFrom(allTypesFromTheAssemblies)
                        .ForEach(map => map.CreateMappings(options));
                }
            );

            static IEnumerable<TypeAndInterfacePair> GetPairs(IEnumerable<Type> types)
                => types.SelectMany(
                    type => type.GetTypeInfo().GetInterfaces(),
                    (type, @interface) => (Type: type, Interface: @interface)
                );

            static IEnumerable<TypeAndInterfacePair> GetMaps(IEnumerable<Type> types, Func<TypeAndInterfacePair, bool> predicate)
                => GetPairs(types)
                    .Where(pair =>
                        pair.Interface.GetTypeInfo().IsGenericType &&
                        pair.Type.IsNotAbsctractOrInterface() &&
                        predicate(pair)
                    );

            static IEnumerable<SourceAndDestinationPair> GetFromMapsFrom(IEnumerable<Type> types)
                => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    .Select(pair => (
                        Source: pair.Interface.GetTypeInfo().GetGenericArguments()[0],
                        Destination: pair.Type
                    ));

            static IEnumerable<SourceAndDestinationPair> GetToMapsFrom(IEnumerable<Type> types)
                => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    .Select(pair => (
                        Source: pair.Type,
                        Destination: pair.Interface.GetTypeInfo().GetGenericArguments()[0])
                    );

            static IEnumerable<IHaveCustomMappings> GetCustomMappingsFrom(IEnumerable<Type> types)
                => GetPairs(types)
                    .Where(pair =>
                        typeof(IHaveCustomMappings).IsAssignableFrom(pair.Type) &&
                        pair.Type.IsNotAbsctractOrInterface()
                    )
                    .Select(typeAndInterfacePair => Activator.CreateInstance(typeAndInterfacePair.Type)!.CastTo<IHaveCustomMappings>());
        });

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<SmartSalonDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("Sql")));

    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<SmartSalonDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection CallAddControllers(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IServiceCollection RegisterConventionalServices(this IServiceCollection services, params Assembly[] assemblies)
        => services.Scan(types =>
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

    public static IServiceCollection RegisterUnconventionalServices(this IServiceCollection services)
        => services.AddSingleton<JwtSecurityTokenHandler>();
}

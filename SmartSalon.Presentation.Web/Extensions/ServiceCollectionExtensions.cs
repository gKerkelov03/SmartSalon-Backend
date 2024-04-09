using TypeAndInterfacePair = (System.Type Type, System.Type Interface);
using SourceAndDestinationPair = (System.Type Source, System.Type Destination);
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Options;
using SmartSalon.Application.Extensions;
using SmartSalon.Data;
using SmartSalon.Data.Seeding;
using SmartSalon.Presentation.Web.Options;
using SmartSalon.Presentation.Web.Options.Versioning;
using SmartSalon.Presentation.Web.Options.Auth;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAllOptions(this IServiceCollection services, IConfiguration config)
        => services
            .Configure<ConnectionStringsOptions>(config.GetSection(ConnectionStringsOptions.SectionName))
            .Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName))
            .Configure<EmailsOptions>(config.GetSection(EmailsOptions.SectionName))

            .ConfigureOptions<AuthenticationOptionsConfigurator>()
            .ConfigureOptions<AuthorizationOptionsConfigurator>()
            .ConfigureOptions<IdentityOptionsConfigurator>()
            .ConfigureOptions<JwtBearerOptionsConfigurator>()

            .ConfigureOptions<ApiVersioningOptionsConfigurator>()
            .ConfigureOptions<ApiExplorerOptionsConfigurator>()
            .ConfigureOptions<SwaggerGenOptionsConfigurator>()

            .ConfigureOptions<ApiBehaviorOptionsConfigurator>()
            .ConfigureOptions<MvcOptionsConfigurator>()
            .ConfigureOptions<CorsOptionsConfigurator>();

    public static IServiceCollection RegisterMapper(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddAutoMapper(expression =>
        {
            var allTypesFromTheAssemblies = assemblies.SelectMany(assembly => assembly.GetExportedTypes());

            expression.CreateProfile
            (
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
                        Source: pair.Interface
                            .GetTypeInfo()
                            .GetGenericArguments()[0],

                        Destination: pair.Type
                    ));

            static IEnumerable<SourceAndDestinationPair> GetToMapsFrom(IEnumerable<Type> types)
                => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    .Select(pair => (
                        Source: pair.Type,
                        Destination: pair.Interface
                            .GetTypeInfo()
                            .GetGenericArguments()[0]
                    ));

            static IEnumerable<IHaveCustomMappings> GetCustomMappingsFrom(IEnumerable<Type> types)
                => GetPairs(types)
                    .Where(pair =>
                        typeof(IHaveCustomMappings).IsAssignableFrom(pair.Type) &&
                        pair.Type.IsNotAbsctractOrInterface()
                    )
                    .Select(typeAndInterfacePair =>
                        Activator
                            .CreateInstance(typeAndInterfacePair.Type)!
                            .CastTo<IHaveCustomMappings>()
                    );
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

    public static IServiceCollection RegisterConventionalServicesFrom(this IServiceCollection services, params Assembly[] assemblies)
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
        => services
            .AddSingleton<ISeeder, DatabaseSeeder>()
            .AddSingleton<JwtSecurityTokenHandler>();
}

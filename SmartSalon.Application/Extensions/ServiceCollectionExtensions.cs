using TypeAndInterfacePair = (System.Type Type, System.Type Interface);
using SourceAndDestinationPair = (System.Type Source, System.Type Destination);
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Options;
using SmartSalon.Application.PipelineBehaviors;
using SmartSalon.Application.ResultObject;
using System.Reflection;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
        => services
            .RegisterMediatR()
            .RegisterValidators()
            .RegisterMapper(assemblies)
            .RegisterRedis(config);

    internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
        => services.AddMediatR(options => options
            .RegisterServicesFromAssembly(typeof(IResult).Assembly)
            .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
            .AddOpenBehavior(typeof(ValidationPipelineBehavior<,>))
            .AddOpenBehavior(typeof(CachingPipelineBehaviour<,>))
        );

    internal static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration config)
        => services.AddStackExchangeRedisCache(options =>
            options.Configuration = config.GetConnectionString(nameof(ConnectionStringOptions.Redis))
        );

    internal static IServiceCollection RegisterValidators(this IServiceCollection services)
        => services.AddValidatorsFromAssembly(typeof(IResult).Assembly, includeInternalTypes: true);

    internal static IServiceCollection RegisterMapper(this IServiceCollection services, params Assembly[] assemblies)
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
                        .ForEach(map => map.CreateMapping(options));
                }
            );

            IEnumerable<TypeAndInterfacePair> GetPairs(IEnumerable<Type> types)
                => types.SelectMany(
                    type => type.GetTypeInfo().GetInterfaces(),
                    (type, @interface) => (Type: type, Interface: @interface)
                );

            IEnumerable<TypeAndInterfacePair> GetMaps(IEnumerable<Type> types, Func<TypeAndInterfacePair, bool> predicate)
                => GetPairs(types)
                    .Where(pair =>
                        pair.Interface.GetTypeInfo().IsGenericType &&
                        pair.Type.IsNotAbsctractOrInterface() &&
                        predicate(pair)
                    );

            IEnumerable<SourceAndDestinationPair> GetFromMapsFrom(IEnumerable<Type> types)
                => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                    .Select(pair => (
                        Source: pair.Interface.GetTypeInfo().GetGenericArguments()[0],
                        Destination: pair.Type
                    ));

            IEnumerable<SourceAndDestinationPair> GetToMapsFrom(IEnumerable<Type> types)
                => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapTo<>))
                    .Select(pair => (
                        Source: pair.Type,
                        Destination: pair.Interface.GetTypeInfo().GetGenericArguments()[0])
                    );

            IEnumerable<IHaveCustomMapping> GetCustomMappingsFrom(IEnumerable<Type> types)
                => GetPairs(types)
                    .Where(pair =>
                        typeof(IHaveCustomMapping).IsAssignableFrom(pair.Type) &&
                        pair.Type.IsNotAbsctractOrInterface()
                    )
                    .Select(typeAndInterfacePair => Activator.CreateInstance(typeAndInterfacePair.Type)!.CastTo<IHaveCustomMapping>());
        });
}

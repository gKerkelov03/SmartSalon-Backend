using System.Reflection;
using AutoMapper;
using SmartSalon.Shared.Extensions;
using TypeAndInterfacePair = (System.Type Type, System.Type Interface);
using SourceAndDestinationPair = (System.Type Source, System.Type Destination);
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Mapping;

public static class AutoMapperConfig
{
    private static IMapper? mapperInstance;

    public static IMapper MapperInstance
    {
        get
        {
            if (mapperInstance is null)
            {
                throw new InvalidOperationException(
                    "You cannot access the mapper instance before calling the RegisterMappings method"
                );
            }

            return mapperInstance;
        }

        set => mapperInstance = value;
    }

    public static void RegisterMappings(params Assembly[] assemblies)
    {
        var profileName = "ReflectionProfile";
        var configurationExpression = new MapperConfigurationExpression();

        var allTypesFromTheAssemblies = assemblies.SelectMany(assembly =>
            assembly.GetExportedTypes()
        );

        configurationExpression.CreateProfile(
            profileName,
            options =>
            {
                GetFromMapsFrom(allTypesFromTheAssemblies)
                    .ForEach(map => options.CreateMap(map.Source, map.Destination));

                GetToMapsFrom(allTypesFromTheAssemblies)
                    .ForEach(map => options.CreateMap(map.Source, map.Destination));

                GetCustomMappingsFrom(allTypesFromTheAssemblies)
                    .ForEach(map => map.CreateMappings(options));
            });

        var mapperConfiguration = new MapperConfiguration(configurationExpression);
        MapperInstance = new Mapper(mapperConfiguration);
    }

    private static IEnumerable<TypeAndInterfacePair> GetPairs(IEnumerable<Type> types)
        => types.SelectMany(
            type => type.GetTypeInfo().GetInterfaces(),
            (type, @interface) => (Type: type, Interface: @interface)
        );

    private static IEnumerable<TypeAndInterfacePair> GetMaps(
        IEnumerable<Type> types,
        Func<TypeAndInterfacePair, bool> predicate
    )
        => GetPairs(types)
            .Where(typeAndInterfacePair =>
            {
                var (type, @interface) = typeAndInterfacePair;

                return @interface.GetTypeInfo().IsGenericType &&
                    type.IsNotAbsctractOrInterface() &&
                    predicate(typeAndInterfacePair);
            });

    private static IEnumerable<SourceAndDestinationPair> GetFromMapsFrom(
        IEnumerable<Type> types
    )
        => GetMaps(types, typeAndInterfacePair =>
                typeAndInterfacePair
                .Interface
                .GetGenericTypeDefinition() == typeof(IMapFrom<>)
            )
            .Select(typeAndInterfacePair =>
            {
                var (type, @interface) = typeAndInterfacePair;

                return (
                    Source: @interface
                        .GetTypeInfo()
                        .GetGenericArguments()[0],

                    Destination: type
                );
            });

    private static IEnumerable<SourceAndDestinationPair> GetToMapsFrom(
        IEnumerable<Type> types
    )
        => GetMaps(types, typeAndInterfacePair =>
                typeAndInterfacePair
                .Interface
                .GetGenericTypeDefinition() == typeof(IMapTo<>)
            )
            .Select(typeAndInterfacePair =>
            {
                var (type, @interface) = typeAndInterfacePair;

                return (
                    Source: type,
                    Destination: @interface
                        .GetTypeInfo()
                        .GetGenericArguments()[0]
                );
            });

    private static IEnumerable<IHaveCustomMappings> GetCustomMappingsFrom(
        IEnumerable<Type> types
    )
        => GetPairs(types)
            .Where(typeAndInterfacePair =>
            {
                var (type, @interface) = typeAndInterfacePair;
                var iHaveCustomMappings = typeof(IHaveCustomMappings);

                return iHaveCustomMappings.IsAssignableFrom(type) &&
                    type.IsNotAbsctractOrInterface();
            })
            .Select(typeAndInterfacePair =>
            {
                var (type, @interface) = typeAndInterfacePair;

                return Activator
                    .CreateInstance(type)!
                    .CastTo<IHaveCustomMappings>();
            });
}

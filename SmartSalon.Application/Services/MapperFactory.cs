using System.Reflection;
using AutoMapper;
using TypeAndInterfacePair = (System.Type Type, System.Type Interface);
using SourceAndDestinationPair = (System.Type Source, System.Type Destination);
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Application.Mapping;

public class MapperFactory : IMapperFactory
{
    public static IMapper? MapperInstance { get; private set; }

    public IMapper CreateMapper(params Assembly[] assemblies)
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
            }
        );

        var mapperConfiguration = new MapperConfiguration(configurationExpression);
        MapperInstance = new Mapper(mapperConfiguration); ;

        return MapperInstance;
    }

    private IEnumerable<TypeAndInterfacePair> GetPairs(IEnumerable<Type> types)
        => types.SelectMany(
            type => type.GetTypeInfo().GetInterfaces(),
            (type, @interface) => (Type: type, Interface: @interface)
        );

    private IEnumerable<TypeAndInterfacePair> GetMaps(IEnumerable<Type> types, Func<TypeAndInterfacePair, bool> predicate)
        => GetPairs(types).Where(pair =>
        {
            var (type, @interface) = pair;

            return @interface.GetTypeInfo().IsGenericType &&
                type.IsNotAbsctractOrInterface() &&
                predicate(pair);
        });

    private IEnumerable<SourceAndDestinationPair> GetFromMapsFrom(IEnumerable<Type> types)
        => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapFrom<>)
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

    private IEnumerable<SourceAndDestinationPair> GetToMapsFrom(IEnumerable<Type> types)
        => GetMaps(types, pair => pair.Interface.GetGenericTypeDefinition() == typeof(IMapTo<>))
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

    private IEnumerable<IHaveCustomMappings> GetCustomMappingsFrom(IEnumerable<Type> types)
        => GetPairs(types).Where(pair =>
        {
            var (type, @interface) = pair;
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

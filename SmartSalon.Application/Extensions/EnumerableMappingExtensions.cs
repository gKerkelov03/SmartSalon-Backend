using System.Collections;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Application.Extensions;

public static class EnumerableMappingExtensions
{
    public static IEnumerable<TDestination> To<TDestination>(this IEnumerable source)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(MapperFactory.MapperInstance);

        foreach (var item in source)
        {
            yield return MapperFactory.MapperInstance.Map<TDestination>(item);
        }
    }
}
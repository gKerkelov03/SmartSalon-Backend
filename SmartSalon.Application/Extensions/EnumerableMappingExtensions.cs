using System.Collections;
using SmartSalon.Application.Mapping;

namespace SmartSalon.Application.Extensions;

public static class EnumerableMappingExtensions
{
    public static List<TDestination> ToListOf<TDestination>(this IEnumerable enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentNullException.ThrowIfNull(MapperFactory.MapperInstance);

        var list = new List<TDestination>();

        foreach (var item in enumerable)
        {
            list.Add(MapperFactory.MapperInstance.Map<TDestination>(item));
        }

        return list;
    }
}
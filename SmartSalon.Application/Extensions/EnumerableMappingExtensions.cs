using System.Collections;
using AutoMapper;

namespace SmartSalon.Application.Extensions;

public static class EnumerableMappingExtensions
{
    public static List<TDestination> ToListOf<TDestination>(this IEnumerable enumerable, IMapper mapper)
    {
        var list = new List<TDestination>();

        foreach (var item in enumerable)
        {
            list.Add(mapper.Map<TDestination>(item));
        }

        return list;
    }
}
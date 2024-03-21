using System.Collections;
using SmartSalon.Shared.Mapping;

namespace SmartSalon.Application.Extensions;

public static class EnumerableMappingExtensions
{
    public static IEnumerable<TDestination> To<TDestination>(
        this IEnumerable source)
    {
        ArgumentNullException.ThrowIfNull(source);

        foreach (var item in source)
        {
            yield return AutoMapperConfig.MapperInstance.Map<TDestination>(item);
        }
    }
}
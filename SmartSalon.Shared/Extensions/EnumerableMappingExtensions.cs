using System.Collections;
using SmartSalon.Shared.Mapping;

namespace SmartSalon.Shared.Extensions;

public static class EnumerableMappingExtensions
{
    public static IEnumerable<TDestination> To<TDestination>(
        this IEnumerable source)
    {
        ArgumentNullException.ThrowIfNull(source);

        foreach (var item in source)
        {
            var mappedValue = AutoMapperConfig.MapperInstance.Map<TDestination>(item);
            yield return mappedValue;
        }
    }
}
using SmartSalon.Shared.Extensions;
using SmartSalon.Shared.Mapping;

namespace SmartSalon.Application.Extensions;

public static class MappingExtensions
{
    public static DestinationType MapTo<DestinationType>(this object origin)
    {
        ArgumentNullException.ThrowIfNull(origin);

        var mappedValue = AutoMapperConfig.MapperInstance.Map<DestinationType>(origin);
        return mappedValue;
    }

    public static TargetType MapFrom<TargetType>(this TargetType target, object source)
    {
        var targetType = target?.GetType();

        source
        .GetType()
        .GetProperties()
        .ForEach(sourceProperty =>
        {
            var targetProperty = targetType?.GetProperty(sourceProperty.Name);
            var sourcePropertyValue = sourceProperty.GetValue(source);

            targetProperty?.SetValue(target, sourcePropertyValue);
        });

        return target;
    }
}
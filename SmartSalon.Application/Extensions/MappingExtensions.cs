using SmartSalon.Application.Mapping;

namespace SmartSalon.Application.Extensions;

public static class MappingExtensions
{
    public static DestinationType MapTo<DestinationType>(this object origin)
    {
        ArgumentNullException.ThrowIfNull(origin);
        ArgumentNullException.ThrowIfNull(MapperFactory.MapperInstance);

        return MapperFactory.MapperInstance.Map<DestinationType>(origin);
    }

    public static TargetType MapFrom<TargetType>(this TargetType target, object source)
    {
        ArgumentNullException.ThrowIfNull(target);
        var targetType = target.GetType();

        source
            .GetType()
            .GetProperties()
            .ForEach(sourceProperty =>
            {
                var targetProperty = targetType.GetProperty(sourceProperty.Name);
                var sourcePropertyValue = sourceProperty.GetValue(source);

                targetProperty?.SetValue(target, sourcePropertyValue);
            });

        return target;
    }

    public static TObject MapAgainst<TObject>(this TObject target, object source)
    {
        var propertiesToChange = source.GetType().GetProperties();
        var targetType = target!.GetType();

        propertiesToChange.ForEach(property =>
        {
            var targetProperty = targetType.GetProperty(property.Name);

            if (targetProperty is null)
            {
                return;
            }

            object valueToSet = property.GetValue(source)!;
            targetProperty.SetValue(target, valueToSet);
        });

        return target;
    }
}
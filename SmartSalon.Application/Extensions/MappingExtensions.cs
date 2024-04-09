
namespace SmartSalon.Application.Extensions;

public static class MappingExtensions
{
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
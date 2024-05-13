using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

internal class EnumConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(Enum);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.CastTo<string>();
        var isNumber = int.TryParse(propertyValueAsString, out var _);

        if (isNumber)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        var convertedValue = Enum.Parse(_targetType, propertyValueAsString);

        return convertedValue;
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class EnumConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => typeof(Enum).IsAssignableFrom(_targetType);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.ToString();
        var isNumber = int.TryParse(propertyValueAsString, out var _);

        if (isNumber)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        var convertedValue = Enum.Parse(_targetType, propertyValueAsString);

        return convertedValue;
    }
}
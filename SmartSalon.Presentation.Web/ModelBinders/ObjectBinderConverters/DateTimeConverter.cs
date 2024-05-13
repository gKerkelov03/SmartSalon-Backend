using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

internal class DateTimeConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(DateTime);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.CastTo<string>();
        var isNotValidDateTime = !DateTime.TryParse(propertyValueAsString, out var dateTime);

        if (isNotValidDateTime)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return dateTime;
    }
}
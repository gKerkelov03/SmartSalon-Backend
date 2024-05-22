using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

internal class TimeOnlyConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(TimeOnly);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var propertyValueAsString = propertyValue.CastTo<string>();
        var isNotValidTime = !TimeOnly.TryParse(propertyValueAsString, out var timeOnly);

        if (isNotValidTime)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return timeOnly;
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class DateOnlyConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(DateOnly);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.ToString();
        var isNotValidDate = !DateOnly.TryParse(propertyValueAsString, out var timeOnly);

        if (isNotValidDate)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid format");
        }

        return timeOnly;
    }
}
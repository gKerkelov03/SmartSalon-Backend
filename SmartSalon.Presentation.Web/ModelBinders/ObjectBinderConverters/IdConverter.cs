using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

internal class IdConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(Id);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.CastTo<string>();
        var isValidId = Id.TryParse(propertyValueAsString, out var id);

        if (!isValidId)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid Id format");
        }

        return id;
    }
}
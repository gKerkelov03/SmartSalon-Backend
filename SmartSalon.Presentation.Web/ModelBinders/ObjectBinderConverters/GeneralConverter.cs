using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class GeneralConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => true;

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var result = System.Convert.ChangeType(propertyValue, _targetType);

        if (result is null)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Cannot be converted to a C# type");
        }

        return result;
    }
}
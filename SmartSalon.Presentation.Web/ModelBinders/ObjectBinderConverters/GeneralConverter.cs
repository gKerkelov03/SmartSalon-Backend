using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class GeneralConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => true;

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        try
        {
            return System.Convert.ChangeType(propertyValue, _targetType);
        }
        catch
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Cannot be converted to a C# type");
            return null;
        }
    }
}
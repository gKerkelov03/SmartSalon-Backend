using Microsoft.AspNetCore.Mvc.ModelBinding;

internal interface IModelConverter
{
    public bool CanConvert();

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue);
}
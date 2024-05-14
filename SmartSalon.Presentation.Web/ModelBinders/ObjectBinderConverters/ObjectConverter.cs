using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class ObjectConverter(Type _targetType) : IModelConverter
{
    private IEnumerable<IModelConverter> converters = [
        new IdConverter(_targetType),
        new DateOnlyConverter(_targetType),
        new EnumConverter(_targetType),
        new CollectionOfIdsConverter(_targetType),
        new TimeOnlyConverter(_targetType),
        new GeneralConverter(_targetType)
    ];

    public bool CanConvert() => true;

    public object Convert(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        foreach (var converter in converters)
        {
            if (converter.CanConvert())
            {
                return converter.Convert(bindingContext, propertyName, propertyValue);
            }
        }

        return null!;
    }
}
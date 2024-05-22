using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

internal class CollectionOfIdsConverter(Type _targetType) : IModelConverter
{
    public bool CanConvert() => _targetType == typeof(IEnumerable<Id>);

    public object Convert(ModelBindingContext bindingContext, string propertyName, object propertyValue)
    {
        var result = new List<Id>();
        var IdConverter = new IdConverter(_targetType);

        foreach (var id in propertyValue.CastTo<IEnumerable>())
        {
            result.Add(IdConverter.Convert(bindingContext, propertyName, id).CastTo<Id>());
        }

        return result;
    }
}
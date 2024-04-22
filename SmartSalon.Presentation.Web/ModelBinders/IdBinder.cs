using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class IdBinder : BaseBinder, IModelBinder, IModelBinderProvider
{
    public Task BindModelAsync(ModelBindingContext context)
    {
        var passedValueForId = GetTheIdRouteParameter(context);
        var id = ConvertToId(context, IdRouteParameterName, passedValueForId);

        if (!context.ModelState.IsValid)
        {
            return Task.CompletedTask;
        }

        context.Result = ModelBindingResult.Success(id);
        return Task.CompletedTask;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var modelType = context.Metadata.ModelType;
        var modelTypeIsId = modelType == typeof(Id);
        var doesntHaveFromQueryAttribute = !modelType.GetCustomAttributes(typeof(FromQueryAttribute), inherit: false).Any();

        if (modelTypeIsId && doesntHaveFromQueryAttribute)
        {
            return this;
        }

        return null;
    }
}
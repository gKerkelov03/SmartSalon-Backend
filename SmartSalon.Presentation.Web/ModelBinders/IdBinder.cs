using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class IdBinder(IdConverter _idConverter) : IModelBinder, IModelBinderProvider
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var passedValueForId = bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();
        var id = _idConverter.Convert(bindingContext, IdRouteParameterName, passedValueForId);

        if (!bindingContext.ModelState.IsValid)
        {
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(id);
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
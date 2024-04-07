using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

public class IdModelBinder : IModelBinder, IModelBinderProvider
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var IdRouteParameterName = IdRoute.Remove(['{', '}']);
        var passedValueForId = bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();

        var actionParameterName = bindingContext.ActionContext
            .ActionDescriptor
            .Parameters
            .First()
            !.Name;

        var actionParameterContainsIdInItsName =
            actionParameterName.Contains(IdRouteParameterName) ||
            actionParameterName == IdRouteParameterName.ToLower();

        if (string.IsNullOrEmpty(passedValueForId) || !actionParameterContainsIdInItsName)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(passedValueForId.ToId());

        return Task.CompletedTask;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.ModelType == typeof(Id) ? this : null;

}
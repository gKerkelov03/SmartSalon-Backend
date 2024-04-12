using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

public class IdBinder : IModelBinder, IModelBinderProvider
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

        var containsIdInItsName =
            actionParameterName.Contains(IdRouteParameterName) ||
            actionParameterName == IdRouteParameterName.ToLower();

        if (string.IsNullOrEmpty(passedValueForId) || !containsIdInItsName)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var isValidGuid = Guid.TryParse(passedValueForId, out var id);

        if (!isValidGuid)
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Success(id);
        }

        return Task.CompletedTask;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
        => context.Metadata.ModelType == typeof(Id) ? this : null;
}
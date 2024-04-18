using SmartSalon.Application.Extensions;

using Microsoft.AspNetCore.Mvc.ModelBinding;

public abstract class BaseBinder
{
    protected readonly string IdRouteParameterName = IdRoute.Remove(['{', '}']);

    protected string? GetTheIdRouteParameter(ModelBindingContext bindingContext)
        => bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();

    protected static Id ConvertToId(ModelBindingContext bindingContext, string propertyName, string? propertyValue)
    {
        var isValidId = Id.TryParse(propertyValue, out var id);

        if (!isValidId)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid Id format");
        }

        return id;
    }
}
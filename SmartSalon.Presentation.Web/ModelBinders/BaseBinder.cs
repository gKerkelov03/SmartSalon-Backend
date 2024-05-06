
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartSalon.Application.Extensions;

public abstract class BaseBinder
{
    protected string? GetTheIdRouteParameter(ModelBindingContext bindingContext)
        => bindingContext.ActionContext.RouteData.Values[IdRouteParameterName]?.ToString();

    protected static Id ConvertToId(ModelBindingContext bindingContext, string propertyName, object? propertyValue)
    {
        var propertyValueAsString = propertyValue?.CastTo<string>();
        var isValidId = Id.TryParse(propertyValueAsString, out var id);

        if (!isValidId)
        {
            bindingContext.ModelState.TryAddModelError(propertyName, "Invalid Id format");
        }

        return id;
    }
}
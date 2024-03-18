using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartSalon.Infrastructure.Filters;

public class ModelOrNotFoundActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var model = objectResult.Value;

            if (model is null)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}

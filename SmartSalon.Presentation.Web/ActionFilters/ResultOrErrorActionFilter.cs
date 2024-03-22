using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Presentation.Web.Filters;

public class ResultOrErrorActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // var isObjectResult = context.Result is ObjectResult;
        // var aasdf = context.Result.CastTo<ObjectResult>().Value is Result;

        // if (
        //     context.Result is ObjectResult objectResult &&
        //     objectResult.Value is Result result
        // )
        // {
        //     if (result.IsSuccess)
        //     {
        //         context.Result = new OkObjectResult(new { IsSuccess = true });
        //     }
        //     else
        //     {
        //         var errorMessages = result.Errors!.Select(error => error.Description);
        //         context.Result = new BadRequestObjectResult(errorMessages);
        //     }
        // }
    }
}

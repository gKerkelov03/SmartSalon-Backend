using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Extensions;

namespace SmartSalon.Presentation.Web.Features;

[ApiController]
[ApiVersion("1.0")]
[Route("Api/V{version:apiVersion}/[controller]")]
[FailureResponse(Status401Unauthorized)]
[FailureResponse(Status400BadRequest)]
// [Authorize]
public abstract class ApiController() : ControllerBase
{
    protected IActionResult CreatedAndLocatedAt(string controllerName, string actionName, Id createdResourceId, object? response = null)
        => CreatedAtAction(
            actionName,
            controllerName.Replace(nameof(Controller), string.Empty),
            new RouteValueDictionary { [IdRoute.Remove(['{', '}'])] = createdResourceId },
            response ?? new { createdResourceId }
        );

    protected IActionResult CreatedAndLocatedAt(string actionName, Id createdResourceId, object? response = null)
        => CreatedAtAction(
            actionName,
            new { Id = createdResourceId },
            response ?? new { createdResourceId }
        );

    protected IActionResult ProblemDetailsOr<TActionResult>(Result result) where TActionResult : IActionResult
    {
        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return Activator.CreateInstance(typeof(TActionResult))!.CastTo<TActionResult>();
    }

    protected IActionResult ProblemDetailsOr(Func<Result, IActionResult> successResponseFactory, Result result)
    {
        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return successResponseFactory(result);
    }

    protected IActionResult ProblemDetailsOr<TValue>(
        Func<Result<TValue>, IActionResult> successResponseFactory,
        Result<TValue> result
    )
    {
        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return successResponseFactory(result);
    }

    protected IActionResult ProblemDetailsOr(Func<IActionResult> successResponseFactory, IResult result)
    {
        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return successResponseFactory();
    }

    private IActionResult ProblemDetails(IResult result)
        => new ObjectResult(result.ToProblemDetails(HttpContext.TraceIdentifier));
}


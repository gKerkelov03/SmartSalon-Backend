using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Presentation.Web.Extensions;

namespace SmartSalon.Presentation.Web.Features;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ProblemDetailsWithErrors), Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetailsWithErrors), Status400BadRequest)]
// [Authorize]
public abstract class ApiController() : ControllerBase
{
    protected IActionResult CreatedAndLocatedAt(string controllerName, string actionName, Id createdResourceId, object? response = null)
        => CreatedAtAction(
            actionName,
            controllerName,
            new { Id = createdResourceId },
            response ?? new { createdResourceId }
        );

    protected IActionResult CreatedAndLocatedAt(string actionName, Id createdResourceId, object? response = null)
        => CreatedAtAction(
            actionName,
            new { Id = createdResourceId },
            response ?? new { createdResourceId }
        );

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

using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.ResultObject;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Presentation.Web.Models.Responses;

namespace SmartSalon.Presentation.Web.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ProblemDetailsWithErrors), Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetailsWithErrors), Status400BadRequest)]
// [Authorize]
public abstract class ApiController() : ControllerBase
{
    protected IActionResult ProblemDetailsOrActionResult(IResult result, Func<IActionResult> successResponseFactory)
    {
        var traceId = HttpContext.TraceIdentifier;

        if (result.IsFailure)
        {
            return new ObjectResult(result.ToProblemDetails(traceId));
        }

        return successResponseFactory();
    }

    protected IActionResult CreatedAndLocatedAt(string actionName, Id createdResourceId, object? response = null)
        => CreatedAtAction(actionName, new { Id = createdResourceId }, response ?? new { createdResourceId });
}


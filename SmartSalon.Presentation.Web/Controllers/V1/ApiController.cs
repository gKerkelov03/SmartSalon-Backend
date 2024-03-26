using MediatR;
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
//TODO: change the default format of errors, IValidationProblemDetailsFactory
public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    public ApiController(ISender sender)
        => _sender = sender;

    protected IActionResult ProblemDetails(IResult result)
        => new ObjectResult(result.ToProblemDetails(HttpContext.TraceIdentifier));

    protected IActionResult CreatedAndLocatedAt(string actionName, Id id, object? response = null)
    {
        if (response is null)
        {
            response = new { Id = id };
        }

        return CreatedAtAction(actionName, new { Id = id }, response);
    }
}


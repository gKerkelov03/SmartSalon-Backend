using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Queries;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Presentation.Web.Controllers.V1;

public class ExampleController : ApiController
{
    public ExampleController(ISender sender) : base(sender) { }

    [Route("example-path")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ExampleEndpoint(ExampleRequest request)
    {
        var response = await _sender.Send(request.MapTo<ExampleQuery>());

        return Ok(response);
    }
}
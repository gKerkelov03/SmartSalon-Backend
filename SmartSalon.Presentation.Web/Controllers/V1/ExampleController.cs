using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Queries;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Presentation.Web.Controllers.V1;

public class ExampleController : ApiController
{
    public ExampleController(ISender sender) : base(sender) { }

    [HttpPost]
    [Route("example-path1")]
    public async Task<IActionResult> ExampleEndpoint1(ExampleRequest request)
    {
        var queryResponse = await _sender.Send(request.MapTo<ExampleQuery>());

        if (queryResponse.IsFailed)
        {
            return BadRequest(queryResponse);
        }

        return Ok(queryResponse);
    }

    [HttpPost]
    [Route("example-path2")]
    public async Task<IActionResult> ExampleEndpoint2(ExampleRequest request)
    {
        var commandResponse = await _sender.Send(request.MapTo<ExampleCommand>());

        if (commandResponse.IsFailed)
        {
            return BadRequest(commandResponse.Errors);
        }

        return Ok(commandResponse);
    }
}
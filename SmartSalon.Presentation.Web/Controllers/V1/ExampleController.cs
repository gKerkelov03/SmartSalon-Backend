using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Queries;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Presentation.Web.Models.Responses;

namespace SmartSalon.Presentation.Web.Controllers.V1;

public class ExampleController : ApiController
{
    public ExampleController(ISender sender) : base(sender) { }

    [HttpPost]
    [Route("example-path1")]
    public async Task<IActionResult> ExampleEndpoint1(ExampleRequest request)
    {
        var responseResult = await _sender.Send(request.MapTo<ExampleQuery>());

        if (responseResult.IsFailed)
        {
            var problemDetails = responseResult.ToProblemDetails();
            return BadRequest(problemDetails);
        }

        var response = responseResult.Value.MapTo<ExampleResponse>();

        return Ok(response);
    }

    [HttpPost]
    [Route("example-path2")]
    public async Task<IActionResult> ExampleEndpoint2(ExampleRequest request)
    {
        var commandResponse = await _sender.Send(request.MapTo<ExampleCommand>());

        if (commandResponse.IsFailed)
        {
            return BadRequest(commandResponse.ToProblemDetails());
        }

        return Ok(commandResponse);
    }
}
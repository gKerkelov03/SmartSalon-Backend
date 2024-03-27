using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Queries;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Models.Responses;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Controllers.V1;

public class ExampleController : ApiController
{
    public ExampleController(ISender sender) : base(sender) { }

    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(typeof(ProblemDetailsWithErrors), Status409Conflict)]
    public async Task<IActionResult> Create(CreateRequest request)
    {
        var command = request.MapTo<CreateCommand>();
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        var response = result.Value.MapTo<CreateResponse>();
        return CreatedAndLocatedAt(nameof(GetById), response.Id);
    }

    [HttpGet]
    [Route(IdRouteParameter)]
    [ProducesResponseType(typeof(GetByIdResponse), Status200OK)]
    [ProducesResponseType(typeof(ProblemDetailsWithErrors), Status404NotFound)]
    public async Task<IActionResult> GetById(Id id)
    {
        var query = new GetByIdQuery(id);
        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        var response = result.Value.MapTo<GetByIdResponse>();

        return Ok(response);
    }

    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(IEnumerable<GetByIdResponse>), Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRequest request)
    {
        var query = request.MapTo<GetAllQuery>();
        var result = await _sender.Send(query);

        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        var response = result.Value.MapTo<IEnumerable<GetByIdResponse>>();

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(typeof(ProblemDetailsWithErrors), Status409Conflict)]
    public async Task<IActionResult> Update(UpdateRequest request)
    {
        var command = request.MapTo<UpdateCommand>();
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return Ok();
    }

    [HttpDelete(IdRouteParameter)]
    [ProducesResponseType(Status204NoContent)]
    public async Task<IActionResult> Delete(Id id)
    {
        var command = new DeleteCommand(id);
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return ProblemDetails(result);
        }

        return NoContent();
    }
}
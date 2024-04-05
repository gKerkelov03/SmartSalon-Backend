using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using SmartSalon.Presentation.Web.Features;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Users.Requests;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Application.Features.Workers.Commands;

namespace SmartSalon.Presentation.Web.Users.Controllers;

public class WorkersController(ISender _mediator) : ApiController
{
    private const string IdRouteParameter = "{workerId}";

    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> CreateWorker(CreateWorkerRequest request)
    {
        var command = request.MapTo<CreateWorkerCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetWorkerById), result.Value.CreatedWorkerId),
            result
        );
    }

    [HttpGet]
    [Route(IdRouteParameter)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetWorkerById(Id workerId)
    {
        var query = new GetWorkerByIdQuery(workerId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(result.Value.MapTo<GetWorkerByIdResponse>()),
            result
        );
    }

    [HttpPatch]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> UpdateWorker(UpdateWorkerRequest request)
    {
        var command = request.MapTo<UpdateWorkerCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRouteParameter)]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> RemoveWorkerFromSalon(Id workerId)
    {
        var command = new RemoveWorkerFromSalonCommand(workerId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPost("Include")]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> AddWorkerToSalon(AddWorkerToSalonRequest request)
    {
        var command = request.MapTo<AddWorkerToSalonCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

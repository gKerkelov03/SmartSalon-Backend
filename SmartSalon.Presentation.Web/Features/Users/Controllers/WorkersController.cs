using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Users.Requests;
using SmartSalon.Application.Features.Users.Queries;
using AutoMapper;
using SmartSalon.Presentation.Web.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace SmartSalon.Presentation.Web.Users.Controllers;

public class WorkersController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsOwnerOrAdminPolicy)]
    public async Task<IActionResult> CreateWorker(CreateWorkerRequest request)
    {
        var command = _mapper.Map<CreateWorkerCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetWorkerById), result.Value.CreatedWorkerId),
            result
        );
    }

    [HttpGet("Search")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOrAdminPolicy)]
    public async Task<IActionResult> SearchForUnemployedWorker(string searchTerm)
    {
        var query = new SearchForUnemployedWorkerQuery(searchTerm);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(result.Value.ToListOf<GetWorkerByIdResponse>(_mapper)),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetWorkerById(Id workerId)
    {
        var query = new GetWorkerByIdQuery(workerId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<GetWorkerByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerPolicy)]
    public async Task<IActionResult> UpdateWorker(Id workerId, UpdateWorkerRequest request)
    {
        var command = _mapper.Map<UpdateWorkerCommand>(request);
        command.WorkerId = workerId;

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch("RemoveFromSalon")]
    [SuccessResponse(Status204NoContent)]
    [Authorize(Policy = IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerPolicy)]
    public async Task<IActionResult> RemoveWorkerFromSalon(Id workerId)
    {
        var command = new RemoveWorkerFromSalonCommand(workerId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPost($"{IdRoute}/SendWorkerInvitationEmail")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOrAdminPolicy)]
    public async Task<IActionResult> SendWorkerInvitationEmail(SendWorkerInvitationEmailRequest request)
    {
        var command = _mapper.Map<SendWorkerInvitationEmailCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch("AddToSalon")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOrAdminPolicy)]
    public async Task<IActionResult> AddWorkerToSalon(AddWorkerToSalonRequest request)
    {
        var command = _mapper.Map<AddWorkerToSalonCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

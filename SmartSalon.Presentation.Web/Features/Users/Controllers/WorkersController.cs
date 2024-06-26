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
    [Authorize(Policy = IsOwnerOrIsAdminPolicy)]
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
    [Authorize(Policy = IsOwnerOrIsAdminPolicy)]
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

    [HttpPatch($"UpdateNickname/{IdRoute}")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsTheSameUserOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateWorkerNickname(UpdateWorkerNicknameRequest request)
    {
        var command = _mapper.Map<UpdateWorkerNicknameCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"UpdateJobTitles/")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsOwnerOfTheSalonOfTheWorkerOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateWorkerJobTitles(UpdateWorkerJobTitlesRequest request)
    {
        var command = _mapper.Map<UpdateWorkerJobTitlesCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"RemoveFromSalon/{IdRoute}")]
    [SuccessResponse(Status204NoContent)]
    [Authorize(Policy = IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerOrIsAdminPolicy)]
    public async Task<IActionResult> RemoveWorkerFromSalon(RemoveWorkerFromSalonRequest request)
    {
        var command = _mapper.Map<RemoveWorkerFromSalonCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPatch("AddToSalon")]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> AddWorkerToSalon(string token)
    {
        var command = new AddWorkerToSalonCommand(token);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

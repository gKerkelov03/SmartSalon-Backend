using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class OwnersController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsAdminPolicy)]
    public async Task<IActionResult> CreateOwner(CreateOwnerRequest request)
    {
        var command = _mapper.Map<CreateOwnerCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetOwnerById), result.Value.CreatedOwnerId),
            result
        );
    }

    [HttpGet("Search")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOrIsAdminPolicy)]
    public async Task<IActionResult> SearchForOwner(string searchTerm)
    {
        var query = new SearchForOwnerQuery(searchTerm);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(result.Value.ToListOf<GetOwnerByIdResponse>(_mapper)),
            result
        );
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetOwnerById(Id ownerId)
    {
        var query = new GetOwnerByIdQuery(ownerId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<GetOwnerByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch($"{IdRoute}/RemoveFromSalon")]
    [SuccessResponse(Status204NoContent)]
    [Authorize(Policy = IsTheSameUserOrIsAdminPolicy)]
    public async Task<IActionResult> RemoveOwnerFromSalon(RemoveOwnerFromSalonRequest request)
    {
        var command = _mapper.Map<RemoveOwnerFromSalonCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPatch("AddToSalon")]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> AddOwnerToSalon(string token)
    {
        var command = new AddOwnerToSalonCommand(token);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

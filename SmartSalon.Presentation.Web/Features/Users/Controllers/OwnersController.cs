using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class OwnersController(ISender _mediator) : ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> CreateOwner(CreateOwnerRequest request)
    {
        var command = request.MapTo<CreateOwnerCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(GetOwnerById), result.Value.CreatedOwnerId),
            result
        );
    }

    [HttpGet("Search")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> SearchForOwner(string searchTerm)
    {
        var query = new SearchForOwnerQuery(searchTerm);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(result.Value.ToListOf<GetOwnerByIdResponse>()),
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
            Ok(result.Value.MapTo<GetOwnerByIdResponse>()),
            result
        );
    }

    [HttpPatch("RemoveFromSalon")]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> RemoveOwnerFromSalon(RemoveOwnerFromSalonRequest request)
    {
        var command = request.MapTo<RemoveOwnerFromSalonCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }

    [HttpPatch("AddToSalon")]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> AddOwnerToSalon(AddOwnerToSalonRequest request)
    {
        var command = request.MapTo<AddOwnerToSalonCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

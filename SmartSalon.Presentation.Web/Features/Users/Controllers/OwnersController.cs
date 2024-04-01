using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class OwnersController(ISender _sender) : ApiController
{
    [HttpPost]
    [SuccessResponse(Status201Created)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> CreateOwner(CreateOwnerRequest request)
    {
        var command = request.MapTo<CreateOwnerCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(UsersController), "GetUserById", result.Value.MapTo<CreateUserResponse>().CreatedUserId),
            result
        );
    }

    [HttpDelete(IdRouteParameter)]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> DeleteOwner(Id ownerId)
    {
        var command = new DeleteOwnerCommand { OwnerId = ownerId };
        var result = await _sender.Send(command);

        return ProblemDetailsOr(NoContent, result);
    }
}

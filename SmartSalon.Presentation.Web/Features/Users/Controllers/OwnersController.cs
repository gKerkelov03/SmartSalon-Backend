using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Commands;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class OwnersController(ISender _sender) : ApiController
{
    [HttpPost]
    [ProducesResponseType(Status201Created)]
    [ProducesResponseType(typeof(ProblemDetailsWithErrors), Status409Conflict)]
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
    [ProducesResponseType(Status204NoContent)]
    public async Task<IActionResult> DeleteOwner(Id ownerId)
    {
        var command = new DeleteOwnerCommand { OwnerId = ownerId };
        var result = await _sender.Send(command);

        return ProblemDetailsOr(NoContent, result);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class UsersController(ISender _mediator) : ApiController
{
    private const string IdRouteParameter = "{userId}";

    [HttpGet]
    [Route(IdRouteParameter)]
    [SuccessResponse<GetUserByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetUserById(Id userId)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(result.Value.MapTo<GetUserByIdResponse>()),
            result
        );
    }

    [HttpPatch]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = request.MapTo<UpdateUserCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch("ChangePassword")]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = request.MapTo<ChangePasswordCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch("ChangeEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
    {
        var command = request.MapTo<ChangeEmailCommand>();
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRouteParameter)]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> DeleteUser(Id userId)
    {
        var command = new DeleteUserCommand(userId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}

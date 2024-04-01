using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class UsersController(ISender _sender) : ApiController
{
    [HttpGet]
    [Route(IdRouteParameter)]
    [SuccessResponse<GetUserByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetUserById(Id userId)
    {
        var query = new GetUserByIdQuery() { UserId = userId };
        var result = await _sender.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(result.Value.MapTo<GetUserByIdResponse>()),
            result
        );
    }

    [HttpPatch]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = request.MapTo<UpdateUserCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(Ok, result);
    }

    [HttpPatch("ChangeEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = request.MapTo<ChangePasswordCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(Ok, result);
    }

    [HttpPatch("ChangePassword")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
    {
        var command = request.MapTo<ChangeEmailCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(Ok, result);
    }

    [HttpDelete(IdRouteParameter)]
    [SuccessResponse(Status204NoContent)]
    public async Task<IActionResult> DeleteUser(Id userId)
    {
        var command = new DeleteUserCommand { UserId = userId };
        var result = await _sender.Send(command);

        return ProblemDetailsOr(NoContent, result);
    }
}

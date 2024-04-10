using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Presentation.Web.Features.Users.Responses;
using AutoMapper;
using SmartSalon.Presentation.Web.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class UsersController(ISender _mediator, IMapper _mapper) : V2ApiController
{
    [HttpGet(IdRoute)]
    [SuccessResponse<GetUserByIdResponse>(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetUserById(Id userId)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr((result) =>
            Ok(_mapper.Map<GetUserByIdResponse>(result.Value)),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> UpdateUser(Id userId, UpdateUserRequest request)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"{IdRoute}/ChangePassword")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> ChangePassword(Id userId, ChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"{IdRoute}/ChangeEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> ChangeEmail(Id userId, ChangeEmailRequest request)
    {
        var command = _mapper.Map<ChangeEmailCommand>(request);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"{IdRoute}/ResetPassword")]
    [SuccessResponse(Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> RestorePassword(Id userId, RestorePasswordRequest request)
    {
        var command = _mapper.Map<RestorePasswordCommand>(request);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status204NoContent)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> DeleteUser(Id userId)
    {
        var command = new DeleteUserCommand(userId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}

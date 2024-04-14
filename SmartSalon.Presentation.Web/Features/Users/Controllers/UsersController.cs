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

public class UsersController(ISender _mediator, IMapper _mapper) : V1ApiController
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
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"{IdRoute}/ChangePassword")]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"{IdRoute}/ChangeEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
    {
        var command = _mapper.Map<ChangeEmailCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPatch($"RestorePassword")]
    [SuccessResponse(Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> RestorePassword(RestorePasswordRequest request)
    {
        var command = _mapper.Map<RestorePasswordCommand>(request);
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

    [HttpPost($"{IdRoute}/SendEmailConfirmationEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> SendEmailConfirmationEmail(Id userId)
    {
        var command = new SendEmailConfirmationEmailCommand(userId);

        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpPost("ConfirmEmail")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsTheSameUserOrAdminPolicy)]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var command = new ConfirmEmailCommand(token);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}

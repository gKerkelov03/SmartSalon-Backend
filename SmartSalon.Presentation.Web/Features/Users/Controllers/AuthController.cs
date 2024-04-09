using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class AuthController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            Ok(_mapper.Map<LoginResponse>(result.Value)),
            result
        );
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        return new OkResult();
        // var result = await _mediator.Send();

        // return ProblemDetailsOr(result =>
        //     Ok(result.Value.MapTo<LoginResponse>()),
        //     result
        // );
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            CreatedAndLocatedAt(nameof(UsersController), "GetUserById", result.Value.RegisteredUserId),
            result
        );
    }
}
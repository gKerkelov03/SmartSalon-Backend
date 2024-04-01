using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Features.Users.Requests;
using SmartSalon.Presentation.Web.Features.Users.Responses;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class AuthController(ISender _sender) : ApiController
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = request.MapTo<LoginCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(result =>
            Ok(result.Value.MapTo<LoginResponse>()),
            result
        );
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = request.MapTo<RegisterCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOr(result =>
            Ok(result.Value.MapTo<LoginResponse>()),
            result
        );
    }
}
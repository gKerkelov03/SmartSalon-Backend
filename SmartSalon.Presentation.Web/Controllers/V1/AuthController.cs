
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Commands;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Presentation.Web.Models.Responses;

namespace SmartSalon.Presentation.Web.Controllers.V1;

//TODO: this is pretty much pseudo code at this point, need to be finished
public class AuthController(
    ISender _sender
) : ApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = request.MapTo<LoginCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOrActionResult(result, () =>
        {
            var response = result.Value.MapTo<LoginResponse>();

            return Ok(response);
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = request.MapTo<LoginCommand>();
        var result = await _sender.Send(command);

        return ProblemDetailsOrActionResult(result, () =>
        {
            var response = result.Value.MapTo<LoginResponse>();

            return Ok(response);
        });
    }
}
﻿global using UsersManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.Users.User>;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;
using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Features.Users.Commands;

public class LoginCommand : ICommand<LoginCommandResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginCommandResponse
{
    public required string JwtToken { get; set; }
}

internal class LoginCommandHandler(UsersManager _usersManager, IJwtTokensGenerator _jwtGenerator)
    : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersManager.FindByEmailAsync(command.Email);

        var isPasswordCorrect = await _usersManager.CheckPasswordAsync(user!, command.Password);

        if (!isPasswordCorrect)
        {
            return Error.Unauthorized;
        }

        if (user is null)
        {
            return Error.NotFound;
        }

        var roles = await _usersManager.GetRolesAsync(user);
        var jwt = _jwtGenerator.GenerateJwt(user.Id, roles);

        return new LoginCommandResponse { JwtToken = jwt };
    }
}

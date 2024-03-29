global using UsersManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.User>;

using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class LoginCommandHandler(IJwtTokensGenerator _jwtGenerator, UsersManager _usersManager)
    : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersManager.FindByEmailAsync(command.Email);
        var isPasswordCorrect = await _usersManager.CheckPasswordAsync(user!, command.Password);

        if (user is null || !isPasswordCorrect)
        {
            return Error.Unauthorized("Invalid email or password");
        }

        var jwt = _jwtGenerator.GenerateFor(user.Id);

        return new LoginCommandResponse() { JwtToken = jwt };
    }
}

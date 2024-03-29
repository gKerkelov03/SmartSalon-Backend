global using ProfileManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.Profile>;

using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class LoginCommandHandler(IJwtTokensGenerator _jwtGenerator, ProfileManager _profileManager)
    : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _profileManager.FindByEmailAsync(command.Email);
        var isPasswordCorrect = await _profileManager.CheckPasswordAsync(user!, command.Password);

        if (user is null || !isPasswordCorrect)
        {
            return Error.Unauthorized("Invalid email or password");
        }

        var jwt = _jwtGenerator.GenerateFor(user.Id);

        return new LoginCommandResponse() { JwtToken = jwt };
    }
}

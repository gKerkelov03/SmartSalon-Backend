using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Commands.Responses;

namespace SmartSalon.Application.Commands;

public class LoginCommand : ICommand<LoginCommandResponse>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

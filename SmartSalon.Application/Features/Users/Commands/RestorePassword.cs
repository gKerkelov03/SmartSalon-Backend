using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

public class RestorePasswordCommand : ICommand
{
    public Id UserId { get; set; }
    public required string NewPassword { get; set; }
}

internal class RestorePasswordCommandHandler(UsersManager _usersManager)
    : ICommandHandler<RestorePasswordCommand>
{
    public async Task<Result> Handle(RestorePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersManager.FindByIdAsync(command.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var resetToken = await _usersManager.GeneratePasswordResetTokenAsync(user);
        var identityResult = await _usersManager.ChangePasswordAsync(user, resetToken, command.NewPassword);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return Result.Success();
    }
}

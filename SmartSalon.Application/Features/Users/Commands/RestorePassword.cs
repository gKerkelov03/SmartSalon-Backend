using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using SmartSalon.Application.ResultObject;

public class RestorePasswordCommand : ICommand
{
    public required string NewPassword { get; set; }
    public required string Token { get; set; }
}

internal class RestorePasswordCommandHandler(
    UsersManager _usersManager,
    IDecryptor _decryptor,
    IOptions<EmailOptions> _options
) : ICommandHandler<RestorePasswordCommand>
{
    public async Task<Result> Handle(RestorePasswordCommand command, CancellationToken cancellationToken)
    {
        var decryptedToken = _decryptor.DecryptTo<RestorePasswordEncryptionModel>(command.Token, _options.Value.EncryptionKey);

        if (decryptedToken is null)
        {
            return new Error("Invalid token");
        }

        var user = await _usersManager.FindByIdAsync(decryptedToken.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var resetToken = await _usersManager.GeneratePasswordResetTokenAsync(user);
        var identityResult = await _usersManager.ResetPasswordAsync(user, resetToken, command.NewPassword);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return Result.Success();
    }
}

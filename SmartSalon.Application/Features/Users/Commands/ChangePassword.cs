using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class ChangePasswordCommand : ICommand
{
    public Id UserId { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}

internal class ChangePasswordCommandHandler(UsersManager _usersManager, IEmailsManager _emailsManager)
    : ICommandHandler<ChangePasswordCommand>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersManager.FindByIdAsync(command.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var identityResult = await _usersManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        var encryptionModel = new RestorePasswordEncryptionModel
        {
            UserId = user.Id
        };

        var viewModel = new RestorePasswordViewModel
        {
            UserFirstName = user.FirstName
        };

        await _emailsManager.SendRestorePasswordEmailAsync(user.Email!, encryptionModel, viewModel);
        return Result.Success();
    }
}

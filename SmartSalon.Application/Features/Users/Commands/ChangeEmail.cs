﻿using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class ChangeEmailCommand(string token) : ICommand
{
    public string Token => token;
}

internal class ChangeEmailCommandHandler(
    UsersManager _usersManager,
    IEncryptionHelper _encryptionHelper,
    IOptions<EmailOptions> _emailOptions
) : ICommandHandler<ChangeEmailCommand>
{
    public async Task<Result> Handle(ChangeEmailCommand command, CancellationToken cancellationToken)
    {
        var decryptedToken = _encryptionHelper.DecryptTo<EmailConfirmationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (decryptedToken is null)
        {
            return new Error("Invalid token");
        }

        var user = await _usersManager.FindByIdAsync(decryptedToken.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var _ = await _usersManager.GenerateChangeEmailTokenAsync(user, decryptedToken.NewEmail);
        var identityResult = await _usersManager.ChangeEmailAsync(user, decryptedToken.NewEmail, _);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return Result.Success();
    }
}


using System.Text.Json;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class ConfirmEmailCommand(string token) : ICommand
{
    public string Token => token;
}

public class ConfirmEmailHandler(
    UsersManager _usersManager,
    IEncryptionHelper _encryptionHelper,
    IOptions<EmailsOptions> _emailOptions
) : ICommandHandler<ConfirmEmailCommand>
{
    public async Task<Result> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        var encryptionModel = _encryptionHelper.DecryptTo<EmailConfirmationEmailEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (encryptionModel is null)
        {
            return new Error("Invalid token");
        }

        var user = await _usersManager.FindByIdAsync(encryptionModel.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var tokenForConfirmation = await _usersManager.GenerateEmailConfirmationTokenAsync(user);
        await _usersManager.ConfirmEmailAsync(user, tokenForConfirmation);

        return Result.Success();
    }
}
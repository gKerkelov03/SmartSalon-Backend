
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class SendEmailConfirmationEmailCommand : ICommand
{
    public required Id UserId { get; set; }
    public required string Token { get; set; }
}

public class SendEmailConfirmationEmailHandler(
    IEmailsManager _emailsManager,
    IEfRepository<User> _users
) : ICommandHandler<SendEmailConfirmationEmailCommand>
{
    public async Task<Result> Handle(SendEmailConfirmationEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.FirstAsync(user => user.Id == command.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        if (user.EmailConfirmed)
        {
            return new Error("Email is already confirmed");
        }

        var encryptionModel = new EmailConfirmationEmailEncryptionModel
        {
            UserId = user.Id
        };

        var viewModel = new EmailConfirmationEmailViewModel
        {
            UserFirstName = user.FirstName
        };

        await _emailsManager.SendEmailConfirmationEmailAsync(user.Email!, encryptionModel, viewModel);

        return Result.Success();
    }
}
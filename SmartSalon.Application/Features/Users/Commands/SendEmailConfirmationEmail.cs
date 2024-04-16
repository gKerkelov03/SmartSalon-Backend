
using AutoMapper;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;
namespace SmartSalon.Application.Features.Users.Commands;

public class SendEmailConfirmationEmailCommand : ICommand, IMapTo<EmailConfirmationEncryptionModel>
{
    public Id UserId { get; set; }
    public required string EmailToBeConfirmed { get; set; }
    public required string Password { get; set; }
}

public class SendEmailConfirmationEmailHandler(
    IEfRepository<User> _users,
    IEmailsManager _emailsManager,
    IMapper _mapper
) : ICommandHandler<SendEmailConfirmationEmailCommand>
{
    public async Task<Result> Handle(SendEmailConfirmationEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _users.FirstOrDefaultAsync(user => user.Id == command.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        if (user.Email == command.EmailToBeConfirmed && user.EmailConfirmed)
        {
            return new Error("Email is already confirmed");
        }

        var encryptionModel = _mapper.Map<EmailConfirmationEncryptionModel>(command);
        var viewModel = new EmailConfirmationViewModel
        {
            UserFirstName = user.FirstName
        };

        await _emailsManager.SendEmailConfirmationEmailAsync(command.EmailToBeConfirmed, encryptionModel, viewModel);

        return Result.Success();
    }
}
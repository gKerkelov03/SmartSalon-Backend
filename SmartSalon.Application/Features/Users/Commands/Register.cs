using AutoMapper;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class RegisterCommand : ICommand<RegisterCommandResponse>, IMapTo<Customer>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required string ProfilePictureUrl { get; set; }
}

public class RegisterCommandResponse(Id id)
{
    public Id RegisteredUserId => id;
}

internal class RegisterCommandHandler(UsersManager _users, IMapper _mapper, IEmailsManager _emailsManager)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FindByEmailAsync(command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var newCustomer = _mapper.Map<Customer>(command);
        newCustomer.UserName = command.Email;

        var identityResultForCreation = await _users.CreateAsync(newCustomer);
        var identityResultForAddingToRole = await _users.AddToRoleAsync(newCustomer, CustomerRoleName);

        if (identityResultForCreation.Failure())
        {
            return new Error(identityResultForCreation.ErrorDescription());
        }

        if (identityResultForAddingToRole.Failure())
        {
            return new Error(identityResultForAddingToRole.ErrorDescription());
        }

        var encryptionModel = new EmailConfirmationEncryptionModel
        {
            UserId = newCustomer.Id,
            EmailToBeConfirmed = command.Email,
            Password = command.Password
        };

        var viewModel = new EmailConfirmationViewModel
        {
            UserFirstName = newCustomer.FirstName
        };

        await _emailsManager.SendEmailConfirmationEmailAsync(newCustomer.Email, encryptionModel, viewModel);

        return new RegisterCommandResponse(newCustomer.Id);
    }
}

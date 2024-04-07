using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
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

public class RegisterCommandResponse
{
    public Id RegisteredUserId { get; set; }

    public RegisterCommandResponse(Id id) => RegisteredUserId = id;
}

internal class RegisterCommandHandler(UsersManager _users)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FindByEmailAsync(command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var customer = command.MapTo<Customer>();
        customer.UserName = command.Email;

        var identityResult = await _users.CreateAsync(customer, command.Password);

        if (identityResult.Failure())
        {
            return new Error(identityResult.ErrorDescription());
        }

        return new RegisterCommandResponse(customer.Id);
    }
}

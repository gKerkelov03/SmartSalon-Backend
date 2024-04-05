using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class RegisterCommand : ICommand<RegisterCommandResponse>, IMapTo<User>
{
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PictureUrl { get; set; }
}

public class RegisterCommandResponse
{
    public Id Id { get; set; }
}

internal class RegisterCommandHandler(IEfRepository<User> _users, IUnitOfWork _unitOfWork)
    : ICommandHandler<RegisterCommand, RegisterCommandResponse>
{
    public async Task<Result<RegisterCommandResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FirstAsync(user => user.Email == command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var customer = command.MapTo<Customer>();
        customer.UserName = command.Email;

        await _users.AddAsync(customer);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new RegisterCommandResponse { Id = customer.Id };
    }
}

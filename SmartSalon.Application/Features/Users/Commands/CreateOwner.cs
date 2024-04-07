using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateOwnerCommand : ICommand<CreateOwnerCommandResponse>
{
    public Id SalonId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class CreateOwnerCommandResponse
{
    public Id CreatedOwnerId { get; set; }
}

internal class CreateOwnerCommandHandler(IEfRepository<User> _users, IUnitOfWork _unitOfWork)
    : ICommandHandler<CreateOwnerCommand, CreateOwnerCommandResponse>
{
    public async Task<Result<CreateOwnerCommandResponse>> Handle(CreateOwnerCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FirstAsync(user => user.Email == command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var owner = command.MapTo<Owner>();
        owner.UserName = command.Email;

        await _users.AddAsync(owner);
        await _unitOfWork.SaveAsync(cancellationToken);

        return new CreateOwnerCommandResponse { CreatedOwnerId = owner.Id };
    }
}

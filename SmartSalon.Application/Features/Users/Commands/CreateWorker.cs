using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateWorkerCommand : ICommand<CreateWorkerCommandResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Nickname { get; set; }
    public required string PhoneNumber { get; set; }
    public required string JobTitle { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Id SalonId { get; set; }
}

public class CreateWorkerCommandResponse
{
    public Id CreatedWorkerId { get; set; }
    public CreateWorkerCommandResponse(Id createdWorkerId) => CreatedWorkerId = createdWorkerId;
}

internal class CreateWorkerCommandHandler(IEfRepository<User> _users, IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICommandHandler<CreateWorkerCommand, CreateWorkerCommandResponse>
{
    public async Task<Result<CreateWorkerCommandResponse>> Handle(CreateWorkerCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FirstAsync(user => user.Email == command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var worker = _mapper.Map<Worker>(command);
        worker.UserName = command.Email;

        await _users.AddAsync(worker);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateWorkerCommandResponse(worker.Id);
    }
}

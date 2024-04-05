using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class AddWorkerToSalonCommand : ICommand<AddWorkerToSalonCommandResponse>
{
    public Id SalonId { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class AddWorkerToSalonCommandResponse
{
    public Id CreatedWorkerId { get; set; }
}

internal class AddWorkerToSalonCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<Worker> _repository)
    : ICommandHandler<AddWorkerToSalonCommand, AddWorkerToSalonCommandResponse>
{
    public async Task<Result<AddWorkerToSalonCommandResponse>> Handle(AddWorkerToSalonCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new AddWorkerToSalonCommandResponse() { CreatedWorkerId = Id.NewGuid() });
    }
}

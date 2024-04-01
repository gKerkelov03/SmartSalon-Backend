using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateWorkerCommand : ICommand<CreateWorkerCommandResponse>
{
    public required Id SalonId { get; set; }

    public required string UserName { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class CreateWorkerCommandResponse
{
    public required Id CreatedWorkerId { get; set; }
}

internal class CreateWorkerCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<Worker> _repository, IPublisher _publisher)
    : ICommandHandler<CreateWorkerCommand, CreateWorkerCommandResponse>
{
    public async Task<Result<CreateWorkerCommandResponse>> Handle(CreateWorkerCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new CreateWorkerCommandResponse() { CreatedWorkerId = Id.NewGuid() });
    }
}

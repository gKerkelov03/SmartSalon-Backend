using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Features.Users.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class DeleteWorkerCommand : ICommand
{
    public Id WorkerId { get; set; }
}

internal class DeleteWorkerCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<Worker> _workersRepository, IPublisher _publisher)
    : ICommandHandler<DeleteWorkerCommand>
{
    public async Task<Result> Handle(DeleteWorkerCommand command, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new UserChangedNotification() { Id = command.WorkerId });

        return await Task.FromResult(Result.Success());
    }
}

using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Features.Users.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class DeleteOwnerCommand : ICommand
{
    public Id OwnerId { get; set; }
}

internal class DeleteOwnerCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<Owner> _repository, IPublisher _publisher)
    : ICommandHandler<DeleteOwnerCommand>
{
    public async Task<Result> Handle(DeleteOwnerCommand command, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new UserChangedNotification() { Id = command.OwnerId });

        return await Task.FromResult(Result.Success());
    }
}

using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class DeleteCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<BookingTime> _repository, IPublisher _publisher) : ICommandHandler<DeleteCommand>
{
    public async Task<Result> Handle(DeleteCommand query, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new ChangedNotification() { Id = query.Id });

        return await Task.FromResult(Result.Success());
    }
}

using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

internal class UpdateCommandHandler(IUnitOfWork _unitOfWork, IEfRepository<BookingTime> _repository, IPublisher _publisher) : ICommandHandler<UpdateCommand>
{
    public async Task<Result> Handle(UpdateCommand query, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new ChangedNotification() { Id = query.Id });

        return await Task.FromResult(Result.Success());
    }
}

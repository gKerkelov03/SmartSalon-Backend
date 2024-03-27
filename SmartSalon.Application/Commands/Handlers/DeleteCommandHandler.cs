using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;
    private readonly IPublisher _publisher;

    public DeleteCommandHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Result> Handle(DeleteCommand query, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new ChangedNotification() { Id = query.Id });

        return await Task.FromResult(Result.Success());
    }
}

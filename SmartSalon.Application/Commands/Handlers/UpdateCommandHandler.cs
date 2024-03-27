using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Notifications;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;
    private readonly IPublisher _publisher;

    public UpdateCommandHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        this._publisher = publisher;
    }

    public async Task<Result> Handle(UpdateCommand query, CancellationToken cancellationToken)
    {
        await _publisher.Publish(new ChangedNotification() { Id = query.Id });

        return await Task.FromResult(Result.Success());
    }
}

using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Commands.Handlers;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public DeleteCommandHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteCommand query, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result.Success());
    }
}

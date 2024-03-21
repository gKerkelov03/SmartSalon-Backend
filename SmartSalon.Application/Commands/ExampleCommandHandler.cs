using FluentResults;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;

namespace SmartSalon.Application.Queries;

public class ExampleCommandHandler : ICommandHandler<ExampleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public ExampleCommandHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Handle(ExampleCommand query, CancellationToken cancellationToken)
    {
        return Result.Ok();
    }
}

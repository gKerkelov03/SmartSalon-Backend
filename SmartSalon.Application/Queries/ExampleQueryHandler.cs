using FluentResults;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;

namespace SmartSalon.Application.Queries;

public class ExampleQueryHandler : IQueryHandler<ExampleQuery, ExampleQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public ExampleQueryHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<ExampleQueryResponse>> Handle(ExampleQuery query, CancellationToken cancellationToken)
    {
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        var bookingTime = new BookingTime()
        {
            From = currentTime,
            To = currentTime
        };
        var response = new ExampleQueryResponse() { ExampleProperty1 = "gosho", ExampleProperty2 = 5 };

        return await Task.FromResult(Result.Ok(response));
    }
}

using FluentResults;
using MediatR;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;

namespace SmartSalon.Application.Queries;

public class ExampleQueryHandler : IQueryHandler<ExampleQuery, BookingTime>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEfRepository<BookingTime> _repository;

    public ExampleQueryHandler(IUnitOfWork unitOfWork, IEfRepository<BookingTime> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<BookingTime>> Handle(ExampleQuery query, CancellationToken cancellationToken)
    {
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);
        // var timeResult = await _repository.FirstAsync(time => time.From == currentTime);

        // if (timeResult is null)
        // {
        //     return Result.Fail(new Error("Invalid something"));
        // }
        Console.WriteLine("doesn't work");
        var bookingTime = new BookingTime()
        {
            From = currentTime,
            To = currentTime
        };

        return await Task.FromResult(Result.Ok(bookingTime));
    }
}

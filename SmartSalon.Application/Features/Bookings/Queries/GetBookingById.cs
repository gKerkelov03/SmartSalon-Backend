using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetBookingByIdQuery(Id id) : IQuery<GetBookingByIdQueryResponse>
{
    public Id BookingId => id;
}

public class GetBookingByIdQueryResponse : IMapFrom<Booking>
{
    public Id Id { get; set; }
    public bool Done { get; set; }
    public required string Note { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public required string ServiceName { get; set; }
    public required string CustomerFirstName { get; set; }
    public required string CustomerLastName { get; set; }
    public required string WorkerNickname { get; set; }
    public required string SalonName { get; set; }
    public required string SalonProfilePictureUrl { get; set; }

    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
    public Id CustomerId { get; set; }
}
internal class GetBookingByIdQueryHandler(IEfRepository<Booking> _bookings, IMapper _mapper)
    : IQueryHandler<GetBookingByIdQuery, GetBookingByIdQueryResponse>
{
    public async Task<Result<GetBookingByIdQueryResponse>> Handle(GetBookingByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _bookings.All
            .Include(booking => booking.Service)
            .Include(booking => booking.Salon)
            .Include(booking => booking.Worker)
            .Include(booking => booking.Customer)
            .ProjectTo<GetBookingByIdQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(booking => booking.Id == query.BookingId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
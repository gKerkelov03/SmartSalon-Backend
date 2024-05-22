using AutoMapper;
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
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
}

internal class GetBookingByIdQueryHandler(IEfRepository<Booking> _bookings, IMapper _mapper)
    : IQueryHandler<GetBookingByIdQuery, GetBookingByIdQueryResponse>
{
    public async Task<Result<GetBookingByIdQueryResponse>> Handle(GetBookingByIdQuery query, CancellationToken cancellationToken)
    {
        var booking = await _bookings.GetByIdAsync(query.BookingId);

        if (booking is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetBookingByIdQueryResponse>(booking);
    }
}
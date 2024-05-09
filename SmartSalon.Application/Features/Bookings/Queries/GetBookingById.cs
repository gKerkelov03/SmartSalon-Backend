using AutoMapper;
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
    public required string CustomerName { get; set; }
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
            .Select(booking => new GetBookingByIdQueryResponse
            {
                Id = booking.Id,
                Done = booking.Done,
                Note = booking.Note,
                Date = booking.Date,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,

                ServiceName = booking.Service!.Name,
                CustomerName = booking.Customer!.FirstName,
                WorkerNickname = booking.Worker!.Nickname,
                SalonName = booking.Salon!.Name,
                SalonProfilePictureUrl = booking.Salon!.ProfilePictureUrl,

                ServiceId = booking.ServiceId,
                SalonId = booking.SalonId,
                WorkerId = booking.WorkerId,
                CustomerId = booking.WorkerId,
            })
            .FirstOrDefaultAsync(booking => booking.Id == query.BookingId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
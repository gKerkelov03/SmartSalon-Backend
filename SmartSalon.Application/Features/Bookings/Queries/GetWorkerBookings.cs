using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetWorkerBookingsQuery(Id id) : IQuery<IEnumerable<GetBookingByIdQueryResponse>>
{
    public Id WorkerId => id;
}

internal class GetWorkerBookingsQueryHandler(IEfRepository<Booking> _bookings, IMapper _mapper)
    : IQueryHandler<GetWorkerBookingsQuery, IEnumerable<GetBookingByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetBookingByIdQueryResponse>>> Handle(GetWorkerBookingsQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _bookings.All
            .Include(booking => booking.Service)
            .Include(booking => booking.Salon)
            .Include(booking => booking.Worker)
            .Include(booking => booking.Customer)
            .Where(booking => booking.WorkerId == query.WorkerId && !booking.Done)
            .ProjectTo<GetBookingByIdQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return queryResponse;
    }
}
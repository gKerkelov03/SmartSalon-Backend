using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Queries;

public class GetCustomerBookingsQuery(Id id) : IQuery<IEnumerable<GetBookingByIdQueryResponse>>
{
    public Id CustomerId => id;
}

internal class GetCustomerBookingsQueryHandler(IEfRepository<Booking> _bookings, IMapper _mapper)
    : IQueryHandler<GetCustomerBookingsQuery, IEnumerable<GetBookingByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetBookingByIdQueryResponse>>> Handle(GetCustomerBookingsQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _bookings.All
            .Include(booking => booking.Service)
            .Include(booking => booking.Salon)
            .Include(booking => booking.Worker)
            .Include(booking => booking.Customer)
            .Where(booking => booking.CustomerId == query.CustomerId && !booking.Done)
            .ProjectTo<GetBookingByIdQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return queryResponse;
    }
}
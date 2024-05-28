using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Queries;

namespace SmartSalon.Presentation.Web.Features.Bookings.Requests;

public class GetAvailableSlotsForBookingRequest : IMapTo<GetAvailableSlotsForBookingQuery>
{
    public DateOnly Date { get; set; }
    public Id ServiceId { get; set; }
    public Id CustomerId { get; set; }
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}

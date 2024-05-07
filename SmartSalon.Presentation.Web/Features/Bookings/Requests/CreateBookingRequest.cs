using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Bookings.Commands;

namespace SmartSalon.Presentation.Web.Features.Bookings.Requests;

public class CreateBookingRequest : IMapTo<CreateBookingCommand>
{
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Id ServiceId { get; set; }
    public Id CustomerId { get; set; }
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
}

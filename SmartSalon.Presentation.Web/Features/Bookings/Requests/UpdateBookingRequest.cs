using SmartBooking.Application.Features.Bookings.Commands;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateBookingRequest : IMapTo<UpdateBookingCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id BookingId { get; set; }
    public Id WorkerId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool Done { get; set; }
    public required string Note { get; set; }
}
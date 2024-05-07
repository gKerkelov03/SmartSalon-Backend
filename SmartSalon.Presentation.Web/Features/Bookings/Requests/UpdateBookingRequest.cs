using SmartBooking.Application.Features.Bookings.Commands;
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateBookingRequest : IMapTo<UpdateBookingCommand>
{
    public Id BookingId { get; set; }
    public Id WorkerId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public bool Done { get; set; }
    public required string Note { get; set; }
}
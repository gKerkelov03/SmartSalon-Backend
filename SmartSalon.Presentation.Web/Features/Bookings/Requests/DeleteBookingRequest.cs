using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Bookings.Requests;

public class DeleteBookingRequest : IMapTo<DeleteBookingCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id BookingId { get; set; }

    public Id SalonId { get; set; }
}
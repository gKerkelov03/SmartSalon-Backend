using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Bookings.Requests;

public class DeleteBookingRequest : IMapTo<DeleteBookingCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id BookingId { get; set; }

    public Id SalonId { get; set; }
}
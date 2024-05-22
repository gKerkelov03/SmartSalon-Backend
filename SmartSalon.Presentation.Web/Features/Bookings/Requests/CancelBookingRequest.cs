using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Bookings.Requests;

public class CancelBookingRequest : IMapTo<CancelBookingCommand>
{
}
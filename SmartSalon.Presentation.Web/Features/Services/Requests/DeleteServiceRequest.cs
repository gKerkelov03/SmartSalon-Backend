using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteServiceRequest : IMapTo<DeleteServiceCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
}
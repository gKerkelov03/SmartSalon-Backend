using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RemoveOwnerFromSalonRequest : IMapTo<RemoveOwnerFromSalonCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
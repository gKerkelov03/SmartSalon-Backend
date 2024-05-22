using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RemoveOwnerFromSalonRequest : IMapTo<RemoveOwnerFromSalonCommand>
{
    [IdRouteParameter]
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
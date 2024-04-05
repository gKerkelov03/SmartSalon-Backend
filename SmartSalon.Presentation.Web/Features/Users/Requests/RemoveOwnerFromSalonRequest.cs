using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RemoveOwnerFromSalonRequest : IMapTo<RemoveOwnerFromSalonCommand>
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Controllers;

public class AddOwnerToSalonRequest : IMapTo<AddOwnerToSalonCommand>
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
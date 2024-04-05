using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Users.Controllers;

public class AddWorkerToSalonRequest : IMapTo<AddWorkerToSalonCommand>
{
    public Id WorkerId { get; set; }

    public Id SalonId { get; set; }
}
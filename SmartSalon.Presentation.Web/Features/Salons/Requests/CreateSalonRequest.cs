using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class CreateSalonRequest : IMapTo<CreateSalonCommand>
{
    public required string Name { get; set; }
    public required string GoogleMapsLocation { get; set; }
}


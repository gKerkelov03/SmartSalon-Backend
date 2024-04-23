using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class CreateSalonRequest : IMapTo<CreateSalonCommand>
{
    [IdRouteParameter]
    public Id WorkerId { get; set; }
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
}


using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class CreateSpecialtyRequest : IMapTo<CreateSpecialtyCommand>
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
}
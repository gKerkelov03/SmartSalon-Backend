using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteSpecialtyRequest : IMapTo<DeleteSpecialtyCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id SpecialtyId { get; set; }
    public Id SalonId { get; set; }
}


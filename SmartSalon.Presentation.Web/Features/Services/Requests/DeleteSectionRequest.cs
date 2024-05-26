using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteSectionRequest : IMapTo<DeleteSectionCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id SectionId { get; set; }
    public Id SalonId { get; set; }
}
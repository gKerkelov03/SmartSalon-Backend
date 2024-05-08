using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Presentation.Web.Attributes;
using SmartSection.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateSectionRequest : IMapTo<UpdateSectionCommand>
{
    [IdRouteParameter]
    public Id SectionId { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
    public int Order { get; set; }
}
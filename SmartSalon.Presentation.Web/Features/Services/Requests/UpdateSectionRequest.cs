using SmartSalon.Application.Abstractions.Mapping;
using SmartSection.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateSectionRequest : IMapTo<UpdateSectionCommand>
{
    public Id SalonId { get; set; }
    public Id SectionId { get; set; }
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
    public int Order { get; set; }
}
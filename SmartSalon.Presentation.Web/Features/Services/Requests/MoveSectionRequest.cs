using SmartSalon.Application.Abstractions.Mapping;
using SmartSection.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class MoveSectionRequest : IMapTo<MoveSectionCommand>
{
    public required Id SectionId { get; set; }
    public Id SalonId { get; set; }
    public required int Order { get; set; }
}
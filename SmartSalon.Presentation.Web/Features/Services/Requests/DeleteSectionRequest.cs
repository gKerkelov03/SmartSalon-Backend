using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteSectionRequest : IMapTo<DeleteSectionCommand>
{
    public Id SectionId { get; set; }
    public Id SalonId { get; set; }
}
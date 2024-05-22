using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class CreateSectionRequest : IMapTo<CreateSectionCommand>
{
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }
    public Id SalonId { get; set; }
}


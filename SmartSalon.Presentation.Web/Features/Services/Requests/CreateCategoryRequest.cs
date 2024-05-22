using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class CreateCategoryRequest : IMapTo<CreateCategoryCommand>
{
    public required string Name { get; set; }
    public Id SalonId { get; set; }
    public Id SectionId { get; set; }
}


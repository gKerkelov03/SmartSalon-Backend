using SmartCategory.Application.Features.Services.Commands;
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class MoveCategoryRequest : IMapTo<MoveCategoryCommand>
{
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
    public Id SectionId { get; set; }
}
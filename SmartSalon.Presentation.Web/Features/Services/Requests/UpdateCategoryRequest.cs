using SmartCategory.Application.Features.Services.Commands;
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateCategoryRequest : IMapTo<UpdateCategoryCommand>
{
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public int Order { get; set; }
}
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteCategoryRequest : IMapTo<DeleteCategoryCommand>
{
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
}
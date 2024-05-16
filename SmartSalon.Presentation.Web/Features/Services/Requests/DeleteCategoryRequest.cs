using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class DeleteCategoryRequest : IMapTo<DeleteCategoryCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
}
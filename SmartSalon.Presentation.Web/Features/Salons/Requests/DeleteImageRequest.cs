using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteImageRequest : IMapTo<DeleteImageCommand>
{
    [IdRouteParameter]
    public Id ImageId { get; set; }
    public Id SalonId { get; set; }
}


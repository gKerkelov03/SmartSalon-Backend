using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteImageRequest : IMapTo<DeleteImageCommand>
{
    public Id ImageId { get; set; }
    public Id SalonId { get; set; }
}


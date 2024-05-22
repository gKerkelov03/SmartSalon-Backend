using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Salons.Requests;

public class DeleteSpecialtyRequest : IMapTo<DeleteSpecialtyCommand>
{
    public Id ImageId { get; set; }
    public Id SalonId { get; set; }
}


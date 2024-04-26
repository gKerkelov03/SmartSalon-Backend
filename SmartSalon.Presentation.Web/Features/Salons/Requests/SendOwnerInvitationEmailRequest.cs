using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class InviteOwnerRequest : IMapTo<InviteOwnerCommand>
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
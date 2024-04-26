using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class InviteWorkerRequest : IMapTo<InviteWorkerCommand>
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendWorkerInvitationEmailRequest : IMapTo<SendWorkerInvitationEmailCommand>
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}
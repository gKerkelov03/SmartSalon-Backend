using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendOwnerInvitationEmailRequest : IMapTo<SendOwnerInvitationEmailCommand>
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
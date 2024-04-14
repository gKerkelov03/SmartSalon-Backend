using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendWorkerInvitationEmailRequest
{
    [IdRouteParameter]
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}
namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendWorkerInvitationEmailRequest
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}
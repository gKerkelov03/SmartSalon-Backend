namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendOwnerInvitationEmailRequest
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}
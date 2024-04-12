using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendEmailConfirmationEmailRequest
{
    [IdRouteParameter]
    public Id UserId { get; set; }
}
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RestorePasswordRequest
{
    [IdRouteParameter]
    public Id UserId { get; set; }

    public required string NewPassword { get; set; }
}
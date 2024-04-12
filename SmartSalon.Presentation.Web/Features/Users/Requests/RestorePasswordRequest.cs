
namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RestorePasswordRequest
{
    public required string NewPassword { get; set; }
    public required string Token { get; set; }
}
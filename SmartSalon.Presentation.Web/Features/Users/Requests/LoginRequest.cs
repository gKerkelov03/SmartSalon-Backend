
namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
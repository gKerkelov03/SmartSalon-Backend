using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class LoginRequest : IMapTo<LoginCommand>
{
    public string Email { get; set; }

    public string? Password { get; set; }
}
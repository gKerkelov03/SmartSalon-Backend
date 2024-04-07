
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class LoginRequest : IMapTo<LoginCommand>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
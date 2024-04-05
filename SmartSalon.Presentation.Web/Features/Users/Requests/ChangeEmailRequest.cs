using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangeEmailRequest : IMapTo<ChangeEmailCommand>
{
    public required string NewEmail { get; set; }
    public required string Password { get; set; }
}

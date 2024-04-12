using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangeEmailRequest : IMapTo<ChangeEmailCommand>
{
    [IdRouteParameter]
    public Id UserId { get; set; }
    public required string NewEmail { get; set; }
    public required string Password { get; set; }
}

using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangeEmailRequest : IMapTo<ChangeEmailCommand>
{
    public string NewEmail { get; set; }

    public string Password { get; set; }
}
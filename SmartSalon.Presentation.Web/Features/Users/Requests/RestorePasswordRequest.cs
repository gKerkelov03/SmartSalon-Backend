
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class RestorePasswordRequest : IMapTo<RestorePasswordCommand>
{
    public required string NewPassword { get; set; }
    public required string Token { get; set; }
}
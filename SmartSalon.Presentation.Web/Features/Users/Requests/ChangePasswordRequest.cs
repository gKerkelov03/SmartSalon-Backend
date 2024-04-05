using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangePasswordRequest : IMapTo<ChangePasswordCommand>
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
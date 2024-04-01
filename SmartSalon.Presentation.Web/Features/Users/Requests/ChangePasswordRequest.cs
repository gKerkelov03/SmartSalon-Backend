using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangePasswordRequest : IMapTo<ChangePasswordCommand>
{
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}
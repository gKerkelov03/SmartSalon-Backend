using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class ChangePasswordRequest : IMapTo<ChangePasswordCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id UserId { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
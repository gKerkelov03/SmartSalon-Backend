using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendEmailConfirmationEmailRequest : IMapTo<SendEmailConfirmationEmailCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id UserId { get; set; }
    public required string EmailToBeConfirmed { get; set; }
    public required string Password { get; set; }
}

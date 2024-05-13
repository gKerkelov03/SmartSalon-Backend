using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class SendEmailConfirmationEmailRequest : IMapTo<SendEmailConfirmationEmailCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id UserId { get; set; }
    public required string EmailToBeConfirmed { get; set; }
    public required string Password { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerNicknameRequest : IMapTo<UpdateWorkerNicknameCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id WorkerId { get; set; }
    public required string Nickname { get; set; }
}


using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Presentation.Web.Attributes;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerRequest : IMapTo<UpdateWorkerCommand>
{
    [IdRouteParameter]
    public Id WorkerId { get; set; }
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
}


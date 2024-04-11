using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerRequest : IMapTo<UpdateWorkerCommand>
{
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
}


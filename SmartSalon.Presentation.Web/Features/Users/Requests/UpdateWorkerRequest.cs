using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Workers.Commands;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerRequest : IMapTo<UpdateWorkerCommand>
{
    public Id WorkerId { get; set; }
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
}


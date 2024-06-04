
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Users.Requests;

public class UpdateWorkerJobTitlesRequest : IMapTo<UpdateWorkerJobTitlesCommand>
{
    public required Id WorkerId { get; set; }
    public required Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}
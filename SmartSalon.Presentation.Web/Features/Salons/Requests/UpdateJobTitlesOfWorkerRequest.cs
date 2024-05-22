using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateJobTitlesOfWorkerRequest : IMapTo<UpdateJobTitlesOfWorkerCommand>
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Presentation.Web.Attributes;
using SmartService.Application.Features.Services.Commands;

namespace SmartSalon.Presentation.Web.Features.Services.Requests;

public class UpdateServiceRequest : IMapTo<UpdateServiceCommand>
{
    [ComesFromRoute(IdRouteParameterName)]
    public Id ServiceId { get; set; }
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double Price { get; set; }
    public int DurationInMinutes { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
    public int Order { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateSalonRequest : IMapTo<UpdateSalonCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int TimePenalty { get; set; }
    public required int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
}
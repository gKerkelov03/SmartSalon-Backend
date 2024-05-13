using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartWorkingTime.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateWorkingTimeRequest : IMapTo<UpdateWorkingTimeCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id WorkingTimeId { get; set; }
    public Id SalonId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
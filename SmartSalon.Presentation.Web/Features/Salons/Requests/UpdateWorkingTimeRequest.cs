using SmartSalon.Application.Abstractions.Mapping;
using SmartWorkingTime.Application.Features.Salons.Commands;

namespace SmartSalon.Presentation.Web.Features.Salons.Requests;

public class UpdateWorkingTimeRequest : IMapTo<UpdateWorkingTimeCommand>
{
    public Id SalonId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public bool IsWorking { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
}
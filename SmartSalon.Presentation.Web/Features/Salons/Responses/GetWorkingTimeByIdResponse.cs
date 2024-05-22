using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Salons.Queries;

namespace SmartSalon.Presentation.Web.Features.Salons.Responses;

public class GetWorkingTimeByIdResponse : IMapFrom<GetWorkingTimeByIdQueryResponse>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }

    public TimeOnly MondayFrom { get; set; }
    public TimeOnly MondayTo { get; set; }

    public TimeOnly TuesdayFrom { get; set; }
    public TimeOnly TuesdayTo { get; set; }

    public TimeOnly WednesdayFrom { get; set; }
    public TimeOnly WednesdayTo { get; set; }

    public TimeOnly ThursdayFrom { get; set; }
    public TimeOnly ThursdayTo { get; set; }

    public TimeOnly FridayFrom { get; set; }
    public TimeOnly FridayTo { get; set; }

    public TimeOnly SaturdayFrom { get; set; }
    public TimeOnly SaturdayTo { get; set; }

    public TimeOnly SundayFrom { get; set; }
    public TimeOnly SundayTo { get; set; }
}
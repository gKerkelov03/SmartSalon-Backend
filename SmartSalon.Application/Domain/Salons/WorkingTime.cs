using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Salons;

public class WorkingTime : BaseEntity
{
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }

    public TimeOnly MondayOpeningTime { get; set; }
    public TimeOnly MondayClosingTime { get; set; }

    public TimeOnly TuesdayOpeningTime { get; set; }
    public TimeOnly TuesdayClosingTime { get; set; }

    public TimeOnly WednesdayOpeningTime { get; set; }
    public TimeOnly WednesdayClosingTime { get; set; }

    public TimeOnly ThursdayOpeningTime { get; set; }
    public TimeOnly ThursdayClosingTime { get; set; }

    public TimeOnly FridayOpeningTime { get; set; }
    public TimeOnly FridayClosingTime { get; set; }

    public TimeOnly SaturdayOpeningTime { get; set; }
    public TimeOnly SaturdayClosingTime { get; set; }

    public TimeOnly SundayOpeningTime { get; set; }
    public TimeOnly SundayClosingTime { get; set; }
}
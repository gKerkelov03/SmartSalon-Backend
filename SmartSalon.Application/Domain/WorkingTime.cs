
using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class WorkingTime : BaseEntity
{
    public Id MondayId { get; set; }
    public virtual TimePeriod? Monday { get; set; }
    public Id TuesdayId { get; set; }
    public virtual TimePeriod? Tuesday { get; set; }
    public Id WednesdayId { get; set; }
    public virtual TimePeriod? Wednesday { get; set; }
    public Id ThursdayId { get; set; }
    public virtual TimePeriod? Thursday { get; set; }
    public Id FridayId { get; set; }
    public virtual TimePeriod? Friday { get; set; }
    public Id SaturdayId { get; set; }
    public virtual TimePeriod? Saturday { get; set; }
    public Id SundayId { get; set; }
    public virtual TimePeriod? Sunday { get; set; }
}
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Services;

namespace SmartSalon.Application.Domain.Bookings;

public class SpecialSlot : BaseEntity
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int ExpirationInDays { get; set; }
    public Id ServiceId { get; set; }
    public Service? Service { get; set; }
}

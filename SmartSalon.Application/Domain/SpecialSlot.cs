using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class SpecialSlot : BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public Id ExpirationInDays { get; set; }
    public Id TimePeriodId { get; set; }
    public TimePeriod? TimePeriod { get; set; }
    public Id ServiceId { get; set; }
    public Service? Service { get; set; }
}

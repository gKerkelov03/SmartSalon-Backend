using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Services;

namespace SmartSalon.Application.Domain;

public class SpecialSlot : BaseEntity
{
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int ExpirationInDays { get; set; }
    public Id ServiceId { get; set; }
    public Service? Service { get; set; }
}

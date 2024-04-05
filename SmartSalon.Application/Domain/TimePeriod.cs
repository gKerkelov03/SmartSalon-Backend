using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class TimePeriod : BaseEntity
{
    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }
}
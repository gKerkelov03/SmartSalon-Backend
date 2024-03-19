using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class BookingTime : BaseEntity
{
    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }
}
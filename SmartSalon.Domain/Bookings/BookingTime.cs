using SmartSalon.Domain.Abstractions;

namespace SmartSalon.Domain.Bookings;

public class BookingTime : BaseEntity
{
    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }
}
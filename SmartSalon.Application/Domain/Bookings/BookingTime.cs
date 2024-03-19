using SmartSalon.Services.Domain.Abstractions;

namespace SmartSalon.Services.Domain.Bookings;

public class BookingTime : BaseEntity
{
    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }
}
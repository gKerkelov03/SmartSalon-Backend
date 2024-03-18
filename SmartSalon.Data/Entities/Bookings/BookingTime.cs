using Microsoft.EntityFrameworkCore;
using SmartSalon.Data.Base;

namespace SmartSalon.Data.Entities.Bookings;

[PrimaryKey(nameof(From), nameof(To))]
public class BookingTime : BaseEntity
{
    public TimeOnly From { get; set; }

    public TimeOnly To { get; set; }
}
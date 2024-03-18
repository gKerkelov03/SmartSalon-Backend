
using SmartSalon.Domain.Abstractions;
using SmartSalon.Domain.Bookings;
using SmartSalon.Domain.Salons;

namespace SmartSalon.Domain.Users;

public class Worker : UserWithProfile
{
    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }

    public virtual IList<Booking>? Calendar { get; set; }
}

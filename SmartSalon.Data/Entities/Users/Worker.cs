
using SmartSalon.Data.Entities.Bookings;
using SmartSalon.Data.Entities.Salons;

namespace SmartSalon.Data.Entities.Users;

public class Worker : UserWithProfile
{
    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }

    public virtual IList<Booking>? Calendar { get; set; }
}

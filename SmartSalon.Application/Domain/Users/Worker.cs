
using SmartSalon.Services.Domain.Abstractions;
using SmartSalon.Services.Domain.Bookings;
using SmartSalon.Services.Domain.Salons;

namespace SmartSalon.Services.Domain.Users;

public class Worker : UserWithProfile
{
    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }

    public virtual IList<Booking>? Calendar { get; set; }
}

using SmartSalon.Data.Base;
using SmartSalon.Data.Entities.Salons;
using SmartSalon.Data.Entities.Users;

namespace SmartSalon.Data.Entities.Bookings;

public class Booking : BaseEntity
{
    public DateOnly Date { get; set; }

    public Id TimeId { get; set; }

    public virtual BookingTime? Time { get; set; }

    public Id CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public Id SalonId { get; set; }

    public virtual Salon? Salon { get; set; }

    public Id WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }
}

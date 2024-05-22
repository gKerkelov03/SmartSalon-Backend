
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Bookings;

namespace SmartSalon.Application.Domain.Users;

public class Customer : User, IBaseEntity
{
    public virtual ICollection<Subscription>? Subscriptions { get; set; }
    public virtual ICollection<Booking>? Bookings { get; set; }
}

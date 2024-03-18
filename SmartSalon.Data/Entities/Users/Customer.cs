
using SmartSalon.Data.Entities.Bookings;
using SmartSalon.Data.Entities.Subscriptions;

namespace SmartSalon.Data.Entities.Users;

public class Customer : UserWithProfile
{
    public virtual IList<Subscription>? OngoingSubscriptions { get; set; }

    public virtual IList<Booking>? ActiveBookings { get; set; }
}

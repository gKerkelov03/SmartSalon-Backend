
using SmartSalon.Domain.Abstractions;
using SmartSalon.Domain.Bookings;
using SmartSalon.Domain.Subscriptions;

namespace SmartSalon.Domain.Users;

public class Customer : UserWithProfile
{
    public virtual IList<Subscription>? OngoingSubscriptions { get; set; }

    public virtual IList<Booking>? ActiveBookings { get; set; }
}

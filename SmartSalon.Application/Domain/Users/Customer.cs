
using SmartSalon.Services.Domain.Abstractions;
using SmartSalon.Services.Domain.Bookings;
using SmartSalon.Services.Domain.Subscriptions;

namespace SmartSalon.Services.Domain.Users;

public class Customer : UserWithProfile
{
    public virtual IList<Subscription>? OngoingSubscriptions { get; set; }

    public virtual IList<Booking>? ActiveBookings { get; set; }
}

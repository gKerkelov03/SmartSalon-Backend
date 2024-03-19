
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Customer : UserWithProfile
{
    public virtual IList<Subscription>? OngoingSubscriptions { get; set; }

    public virtual IList<Booking>? ActiveBookings { get; set; }
}

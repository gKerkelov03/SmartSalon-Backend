
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Customer : User, IBaseEntity
{
    public virtual ICollection<Subscription>? OngoingSubscriptions { get; set; }

    public virtual ICollection<Booking>? ActiveBookings { get; set; }
}

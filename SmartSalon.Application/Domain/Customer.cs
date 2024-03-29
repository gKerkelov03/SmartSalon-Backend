
namespace SmartSalon.Application.Domain;

public class Customer : User
{
    public virtual ICollection<Subscription>? OngoingSubscriptions { get; set; }

    public virtual ICollection<Booking>? ActiveBookings { get; set; }
}

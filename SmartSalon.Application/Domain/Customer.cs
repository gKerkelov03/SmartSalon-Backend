
namespace SmartSalon.Application.Domain;

public class Customer : Profile
{
    public virtual ICollection<Subscription>? OngoingSubscriptions { get; set; }

    public virtual ICollection<Booking>? ActiveBookings { get; set; }
}

using SmartSalon.Domain.Bookings;
using SmartSalon.Domain.Abstractions;

namespace SmartSalon.Domain.Subscriptions;

public class SpecialSlot : BaseEntity
{
    public Id BookingTimeId { get; set; }

    public virtual BookingTime? BookingTime { get; set; }

    public virtual IList<Subscription>? Subscription { get; set; }
}
using SmartSalon.Services.Domain.Bookings;
using SmartSalon.Services.Domain.Abstractions;

namespace SmartSalon.Services.Domain.Subscriptions;

public class SpecialSlot : BaseEntity
{
    public Id BookingTimeId { get; set; }

    public virtual BookingTime? BookingTime { get; set; }

    public virtual IList<Subscription>? Subscription { get; set; }
}
using SmartSalon.Data.Base;
using SmartSalon.Data.Entities.Bookings;

namespace SmartSalon.Data.Entities.Subscriptions;

public class SpecialSlot : BaseEntity
{
    public Id BookingTimeId { get; set; }

    public virtual BookingTime? BookingTime { get; set; }

    public virtual IList<Subscription>? Subscription { get; set; }
}
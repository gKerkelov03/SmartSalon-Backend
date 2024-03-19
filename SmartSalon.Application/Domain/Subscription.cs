using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Subscription : BaseEntity
{
    public int TimePenaltyInDays { get; set; }

    public int AllowedBookingsInAdvance { get; set; }

    public SubscriptionTier Tier { get; set; }

    public SubscriptionDuration Duration { get; set; }

    public Id SalonId { get; set; }

    public virtual Salon? Salon { get; set; }

    public virtual IList<SpecialSlot>? SpecialSlots { get; set; }

    public virtual IList<Customer>? ActiveCustomers { get; set; }

    public virtual IList<SalonService>? ServicesIncluded { get; set; }
}

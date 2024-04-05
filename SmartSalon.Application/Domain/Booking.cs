using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain;

public class Booking : BaseEntity
{
    public DateOnly Date { get; set; }
    public Id TimePeriodId { get; set; }
    public virtual TimePeriod? TimePeriod { get; set; }
    public Id ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    public Id CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
    public Id WorkerId { get; set; }
    public virtual Worker? Worker { get; set; }
}

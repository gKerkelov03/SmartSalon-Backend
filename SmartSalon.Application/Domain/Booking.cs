using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Booking : BaseEntity
{
    public DateOnly Date { get; set; }

    public Id TimeId { get; set; }

    public virtual BookingTime? Time { get; set; }

    public Id CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public Id SalonId { get; set; }

    public virtual Salon? Salon { get; set; }

    public Id WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }
}

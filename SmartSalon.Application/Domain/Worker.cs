
namespace SmartSalon.Application.Domain;

public class Worker : User
{
    public Id? SalonId { get; set; }

    public Salon? Salon { get; set; }

    public virtual ICollection<Booking>? Calendar { get; set; }
}

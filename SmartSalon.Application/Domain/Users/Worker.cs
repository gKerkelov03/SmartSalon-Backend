
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Users;

public class Worker : User, IBaseEntity
{
    public Id? SalonId { get; set; }
    public Salon? Salon { get; set; }
    public required string JobTitle { get; set; }
    public required string Nickname { get; set; }
    public virtual ICollection<Booking>? Calendar { get; set; }
}

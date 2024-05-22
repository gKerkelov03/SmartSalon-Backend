
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Bookings;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Users;

public class Worker : User, IBaseEntity
{
    public required string Nickname { get; set; }
    public virtual ICollection<Booking>? Calendar { get; set; }
    public virtual ICollection<JobTitle>? JobTitles { get; set; }
    public ICollection<Salon>? Salons { get; set; }
}

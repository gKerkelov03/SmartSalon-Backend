
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Users;

public class Owner : User, IBaseEntity
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

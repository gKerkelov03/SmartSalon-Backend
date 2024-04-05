
using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Users;

public class Owner : User, IBaseEntity
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

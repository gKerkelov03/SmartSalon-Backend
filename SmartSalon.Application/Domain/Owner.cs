
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Owner : User, IBaseEntity
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

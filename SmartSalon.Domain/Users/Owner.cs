using SmartSalon.Domain.Abstractions;
using SmartSalon.Domain.Salons;

namespace SmartSalon.Domain.Users;

public class Owner : UserWithProfile
{
    public virtual IList<Salon>? Salons { get; set; }
}

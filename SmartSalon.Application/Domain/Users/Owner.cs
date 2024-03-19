using SmartSalon.Services.Domain.Abstractions;
using SmartSalon.Services.Domain.Salons;

namespace SmartSalon.Services.Domain.Users;

public class Owner : UserWithProfile
{
    public virtual IList<Salon>? Salons { get; set; }
}

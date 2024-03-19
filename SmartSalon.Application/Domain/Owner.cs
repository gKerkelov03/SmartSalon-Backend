using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Owner : UserWithProfile
{
    public virtual IList<Salon>? Salons { get; set; }
}

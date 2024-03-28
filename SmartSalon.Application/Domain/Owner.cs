using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Owner : UserWithProfile
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

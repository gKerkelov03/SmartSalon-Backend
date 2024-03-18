using SmartSalon.Domain.Abstractions;

namespace SmartSalon.Domain.Salons;

public class SalonSpecialty : BaseEntity
{
    public required string Description { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
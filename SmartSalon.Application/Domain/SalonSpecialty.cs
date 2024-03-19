using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain.Salons;

public class SalonSpecialty : BaseEntity
{
    public required string Description { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class SalonSpecialty : BaseEntity
{
    public required string Description { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
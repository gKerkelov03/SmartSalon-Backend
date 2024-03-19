using SmartSalon.Services.Domain.Abstractions;

namespace SmartSalon.Services.Domain.Salons;

public class SalonSpecialty : BaseEntity
{
    public required string Description { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
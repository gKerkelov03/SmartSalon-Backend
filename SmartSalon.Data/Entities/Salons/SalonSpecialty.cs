using SmartSalon.Data.Base;

namespace SmartSalon.Data.Entities.Salons;

public class SalonSpecialty : BaseEntity
{
    public required string Description { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
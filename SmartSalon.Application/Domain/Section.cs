using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain;

public class Section : BaseEntity
{
    public required string Name { get; set; }

    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
}
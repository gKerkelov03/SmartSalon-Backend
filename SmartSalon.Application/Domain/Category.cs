using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
    public Id? SectionId { get; set; }
    public Section? Section { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
}
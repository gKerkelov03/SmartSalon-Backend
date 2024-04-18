using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Services;

public class ServiceCategory : BaseEntity
{
    public required string Name { get; set; }
    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
    public Id? SectionId { get; set; }
    public ServiceSection? Section { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
}
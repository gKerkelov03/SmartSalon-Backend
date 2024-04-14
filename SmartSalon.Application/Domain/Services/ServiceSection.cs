using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Services;

public class ServiceSection : BaseEntity
{
    public required string Name { get; set; }
    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
    public virtual ICollection<ServiceCategory>? Categories { get; set; }
}
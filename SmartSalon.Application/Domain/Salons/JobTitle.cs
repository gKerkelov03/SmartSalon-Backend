using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain.Salons;

public class JobTitle : DeletableEntity
{
    public required string Name { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
    public virtual ICollection<Worker>? Workers { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
}

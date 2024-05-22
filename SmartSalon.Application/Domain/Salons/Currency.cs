using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Salons;

public class Currency : BaseEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public virtual ICollection<Salon>? Salons { get; set; }
}
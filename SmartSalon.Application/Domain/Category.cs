using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    public virtual ICollection<Service>? Services { get; set; }
}
using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain;

public class Section : BaseEntity
{
    public required string Name { get; set; }

    public virtual ICollection<Category>? Categories { get; set; }
}
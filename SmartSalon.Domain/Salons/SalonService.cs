
using SmartSalon.Domain.Abstractions;

namespace SmartSalon.Domain.Salons;

public class SalonService : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required double Price { get; set; }

    public required int DurationInMinutes { get; set; }

    public virtual IList<Salon>? Salons { get; set; }
}
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Salon : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Location { get; set; }

    public bool SubscriptionsEnabled { get; set; }

    public bool WorkersCanMoveBookings { get; set; }

    public bool WorkersCanSetNonWorkingPeriods { get; set; }

    public virtual ICollection<Owner>? Owners { get; set; }

    public virtual ICollection<Worker>? Workers { get; set; }

    public virtual ICollection<SalonSpecialty>? SalonSpecialties { get; set; }

    public virtual ICollection<SalonService>? Services { get; set; }

    public virtual ICollection<Image>? Images { get; set; }
}
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

    public Id OwnerId { get; set; }

    public virtual Owner? Owner { get; set; }

    public virtual IList<Worker>? Workers { get; set; }

    public virtual IList<SalonSpecialty>? SalonSpecialties { get; set; }

    public virtual IList<SalonService>? Services { get; set; }

    public virtual IList<Image>? Images { get; set; }
}
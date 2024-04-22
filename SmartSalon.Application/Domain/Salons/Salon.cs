using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain.Salons;

public class Salon : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int DefaultTimePenalty { get; set; }
    public required int DefaultBookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id? WorkingTimeId { get; set; }
    public SalonWorkingTime? WorkingTime { get; set; }
    public Id? CurrencyId { get; set; }
    public SalonCurrency? Currency { get; set; }
    public virtual ICollection<Owner>? Owners { get; set; }
    public virtual ICollection<Worker>? Workers { get; set; }
    public virtual ICollection<SalonSpecialty>? Specialties { get; set; }
    public virtual ICollection<ServiceSection>? Sections { get; set; }
    public virtual ICollection<ServiceCategory>? Categories { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
    public virtual ICollection<SalonImage>? Images { get; set; }
}

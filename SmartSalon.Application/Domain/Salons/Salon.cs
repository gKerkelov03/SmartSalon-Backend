using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain.Salons;

public class Salon : DeletableEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public int TimePenalty { get; set; }
    public int BookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
    public Id WorkingTimeId { get; set; }
    public WorkingTime? WorkingTime { get; set; }
    public Id MainCurrencyId { get; set; }
    public Currency? MainCurrency { get; set; }
    public ICollection<Currency>? AcceptedCurrencies { get; set; }
    public virtual ICollection<Owner>? Owners { get; set; }
    public virtual ICollection<Worker>? Workers { get; set; }
    public virtual ICollection<Specialty>? Specialties { get; set; }
    public virtual ICollection<Section>? Sections { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
    public virtual ICollection<Image>? Images { get; set; }
    public virtual ICollection<JobTitle>? JobTitles { get; set; }
}

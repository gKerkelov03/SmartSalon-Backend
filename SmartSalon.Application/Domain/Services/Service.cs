using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Services;

public class Service : DeletableEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required int Order { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
    public Id CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}
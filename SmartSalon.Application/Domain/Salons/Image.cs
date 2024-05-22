using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Salons;

public class Image : DeletableEntity
{
    public required string Url { get; set; }
    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
}
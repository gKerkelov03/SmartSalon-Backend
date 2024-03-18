using SmartSalon.Domain.Abstractions;
using SmartSalon.Domain.Salons;

namespace SmartSalon.Domain;

public class Image : BaseEntity
{
    public required string Url { get; set; }

    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }
}
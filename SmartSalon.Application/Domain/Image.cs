using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Image : BaseEntity
{
    public required string Url { get; set; }

    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }
}
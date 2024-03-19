using SmartSalon.Services.Domain.Abstractions;
using SmartSalon.Services.Domain.Salons;

namespace SmartSalon.Services.Domain.Users;

public class Image : BaseEntity
{
    public required string Url { get; set; }

    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }
}
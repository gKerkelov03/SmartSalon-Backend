using SmartSalon.Data.Base;
using SmartSalon.Data.Entities.Salons;

namespace SmartSalon.Data.Entities;

public class Image : BaseEntity
{
    public required string Url { get; set; }

    public Id SalonId { get; set; }

    public Salon? Salon { get; set; }
}
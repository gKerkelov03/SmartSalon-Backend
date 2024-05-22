using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Services;

public class Section : DeletableEntity
{
    public required string Name { get; set; }
    public required int Order { get; set; }
    public required string PictureUrl { get; set; }
    public Id SalonId { get; set; }
    public Salon? Salon { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
}
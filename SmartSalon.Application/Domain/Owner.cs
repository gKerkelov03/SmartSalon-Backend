
namespace SmartSalon.Application.Domain;

public class Owner : Profile
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

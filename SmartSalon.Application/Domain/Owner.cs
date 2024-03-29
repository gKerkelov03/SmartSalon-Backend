
namespace SmartSalon.Application.Domain;

public class Owner : User
{
    public virtual ICollection<Salon>? Salons { get; set; }
}

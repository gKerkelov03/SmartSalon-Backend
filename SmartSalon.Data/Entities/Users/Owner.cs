using SmartSalon.Data.Entities.Salons;

namespace SmartSalon.Data.Entities.Users;

public class Owner : UserWithProfile
{
    public virtual IList<Salon>? Salons { get; set; }
}

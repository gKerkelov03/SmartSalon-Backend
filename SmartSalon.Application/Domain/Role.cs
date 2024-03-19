
namespace SmartSalon.Application.Domain;

public class Role
{
    public required string Name { get; set; }

    public virtual IList<UserProfile>? Users { get; set; }
}
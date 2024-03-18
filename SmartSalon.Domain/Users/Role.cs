
namespace SmartSalon.Domain.Users;

//TODO: ask if it is fine for role to be located in this folder
public class Role
{
    public required string Name { get; set; }

    public virtual IList<UserProfile>? Users { get; set; }
}
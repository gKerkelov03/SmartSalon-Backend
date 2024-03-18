using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Data.Entities.Users;

//TODO: ask if it is fine for role to be located in this folder
public class Role : IdentityRole<Id>
{
    public Role(string name) : base(name) { }

    public virtual IList<UserProfile>? Users { get; set; }
}
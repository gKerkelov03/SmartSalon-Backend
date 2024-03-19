
using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Application.Domain;

public class Role : IdentityRole<Id>
{
    public Role(string name) : base(name) { }


    public virtual IList<UserProfile>? Users { get; set; }
}
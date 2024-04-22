
using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Application.Domain.Users;

public class Role : IdentityRole<Id>
{
    public Role(string name) : base(name) { }
}
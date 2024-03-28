using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class Profile : IdentityUser<Id>, IDeletableEntity<Id>
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public Image? Picture { get; set; }

    public Id RoleId { get; set; }

    public Role? Role { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}

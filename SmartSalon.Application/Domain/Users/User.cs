using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Application.Domain.Users;

public class User : IdentityUser<Id>, IDeletableEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public Id? DeletedBy { get; set; }
}

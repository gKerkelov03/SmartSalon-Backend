using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Users;

public class User : IdentityUser<Id>, IBaseEntity
{
    public User() => Id = Id.NewGuid();

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public string? UserType { get; set; }

    public override required string Email { get; set; }
    public override required string PhoneNumber { get; set; }
    public override required string UserName { get; set; }
}

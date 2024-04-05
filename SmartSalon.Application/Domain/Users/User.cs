using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Base;

namespace SmartSalon.Application.Domain.Users;

public class User : IdentityUser<Id>, IDeletableEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Id? ProfilePictureId { get; set; }
    public Image? ProfilePicture { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public Id? DeletedBy { get; set; }
}

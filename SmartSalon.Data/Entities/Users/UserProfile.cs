using Microsoft.AspNetCore.Identity;
using SmartSalon.Data.Base.Abstractions;
namespace SmartSalon.Data.Entities.Users;

public class UserProfile : IdentityUser<Id>, IDeletableEntity<Id>
{
    private const string blankProfilePictureUrl =
        "https://res.cloudinary.com/donhvedgr/image/upload/v1663345678/mkunrvxxbwrdovxkwtdm.webp";

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string ProfilePictureUrl { get; set; } = blankProfilePictureUrl;

    public required Id RoleId { get; set; }

    public Role? Role { get; set; }

    public required DateTime CreatedOn { get; set; }

    public Id? CreatedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public Id? LastModifiedBy { get; set; }

    public required bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}

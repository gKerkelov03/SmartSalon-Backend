using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class UserProfile : IdentityUser<Id>, IBaseEntity<Id>
{
    private const string blankProfilePictureUrl =
        "https://res.cloudinary.com/donhvedgr/image/upload/v1663345678/mkunrvxxbwrdovxkwtdm.webp";

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string ProfilePictureUrl { get; set; } = blankProfilePictureUrl;

    public Id RoleId { get; set; }

    public Role? Role { get; set; }

    public required DateTime CreatedOn { get; set; }

    public Id? CreatedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public Id? LastModifiedBy { get; set; }

    public required bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}

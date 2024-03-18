
using System.ComponentModel.DataAnnotations.Schema;
using SmartSalon.Data.Base;

namespace SmartSalon.Data.Entities.Users;

public abstract class UserWithProfile : BaseEntity
{
    [ForeignKey(nameof(UserProfile))]
    public required Id UserProfileId;

    public virtual UserProfile? UserProfile { get; set; }
}

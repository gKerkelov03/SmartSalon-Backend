
using System.ComponentModel.DataAnnotations.Schema;
using SmartSalon.Domain.Users;

namespace SmartSalon.Domain.Abstractions;

public abstract class UserWithProfile : BaseEntity
{
    [ForeignKey(nameof(UserProfile))]
    public required Id UserProfileId;

    public virtual UserProfile? UserProfile { get; set; }
}

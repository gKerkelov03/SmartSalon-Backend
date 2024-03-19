
using System.ComponentModel.DataAnnotations.Schema;
using SmartSalon.Services.Domain.Users;

namespace SmartSalon.Services.Domain.Abstractions;

public abstract class UserWithProfile : BaseEntity
{
    [ForeignKey(nameof(UserProfile))]
    public required Id UserProfileId;

    public virtual UserProfile? UserProfile { get; set; }
}

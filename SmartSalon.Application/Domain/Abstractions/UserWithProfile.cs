
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSalon.Application.Domain.Abstractions;

public abstract class UserWithProfile : BaseEntity
{
    [ForeignKey(nameof(UserProfile))]
    public required Id UserProfileId;

    public virtual UserProfile? UserProfile { get; set; }
}

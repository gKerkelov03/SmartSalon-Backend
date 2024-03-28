
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSalon.Application.Domain.Abstractions;

public abstract class UserWithProfile : BaseEntity
{
    [ForeignKey(nameof(Profile))]
    public Id ProfileId;

    public virtual Profile? Profile { get; set; }
}

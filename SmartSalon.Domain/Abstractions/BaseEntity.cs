using System.ComponentModel.DataAnnotations;

namespace SmartSalon.Domain.Abstractions;

public abstract class BaseEntity : IBaseEntity<Id>
{
    [Key]
    public Id Id { get; set; } = Id.NewGuid();
}

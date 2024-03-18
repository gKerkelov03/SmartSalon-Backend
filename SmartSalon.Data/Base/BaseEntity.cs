using System.ComponentModel.DataAnnotations;
using SmartSalon.Data.Base.Abstractions;

namespace SmartSalon.Data.Base;

public abstract class BaseEntity : IBaseEntity<Id>
{
    [Key]
    public Id Id { get; set; } = Id.NewGuid();
}

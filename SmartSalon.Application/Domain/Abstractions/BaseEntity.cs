
namespace SmartSalon.Application.Domain.Abstractions;

public abstract class BaseEntity : IBaseEntity
{
    public Id Id { get; set; } = Id.NewGuid();
}

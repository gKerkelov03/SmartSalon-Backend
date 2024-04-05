
namespace SmartSalon.Application.Domain.Base;

public abstract class BaseEntity : IBaseEntity
{
    public Id Id { get; set; } = Id.NewGuid();
}

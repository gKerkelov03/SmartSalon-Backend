
namespace SmartSalon.Application.Domain.Abstractions;

public abstract class BaseEntity
{
    public Id Id { get; set; } = Id.NewGuid();
}

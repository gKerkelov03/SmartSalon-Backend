
namespace SmartSalon.Application.Domain.Abstractions;

public abstract class BaseEntity : IBaseEntity<Id>
{
    public Id Id { get; set; }
}

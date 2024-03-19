
namespace SmartSalon.Application.Domain.Abstractions;

public interface IBaseEntity<TKey>
{
    public TKey Id { get; set; }
}

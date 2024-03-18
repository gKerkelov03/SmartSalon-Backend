namespace SmartSalon.Domain.Abstractions;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}
namespace SmartSalon.Services.Domain.Abstractions;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}
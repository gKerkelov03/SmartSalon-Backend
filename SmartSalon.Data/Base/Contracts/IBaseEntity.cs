namespace SmartSalon.Data.Base.Abstractions;

internal interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}
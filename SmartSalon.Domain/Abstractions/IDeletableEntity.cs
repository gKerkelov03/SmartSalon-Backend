namespace SmartSalon.Domain.Abstractions;

public interface IDeletableEntity<TKey> where TKey : struct
{
    bool IsDeleted { get; set; }

    TKey? DeletedBy { get; set; }

    DateTime? DeletedOn { get; set; }
}

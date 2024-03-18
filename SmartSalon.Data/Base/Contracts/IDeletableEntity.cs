namespace SmartSalon.Data.Base.Abstractions;

internal interface IDeletableEntity<TKey> where TKey : struct
{
    bool IsDeleted { get; set; }

    TKey? DeletedBy { get; set; }

    DateTime? DeletedOn { get; set; }
}

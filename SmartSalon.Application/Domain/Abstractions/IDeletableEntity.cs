
namespace SmartSalon.Application.Domain.Abstractions;

public interface IDeletableEntity<TKey> : IBaseEntity<TKey>
{
    DateTime DeletedOn { get; set; }

    Id DeletedBy { get; set; }

    bool IsDeleted { get; set; }
}

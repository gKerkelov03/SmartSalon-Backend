
namespace SmartSalon.Domain.Abstractions;

public abstract class DeletableEntity : IDeletableEntity<Id>
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}

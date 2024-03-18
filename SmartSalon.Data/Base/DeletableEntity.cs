using SmartSalon.Data.Base.Abstractions;

namespace SmartSalon.Data.Base;

internal abstract class DeletableEntity : IDeletableEntity<Id>
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }
}

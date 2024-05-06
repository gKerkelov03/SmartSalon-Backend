

namespace SmartSalon.Application.Domain.Base;

public abstract class DeletableEntity : BaseEntity, IDeletableEntity
{
    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}



namespace SmartSalon.Application.Domain.Abstractions;

public interface IDeletableEntity
{
    public DateTimeOffset? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}

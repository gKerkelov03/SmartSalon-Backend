
namespace SmartSalon.Application.Domain.Base;

public interface IDeletableEntity : IBaseEntity
{
    public DateTimeOffset? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}

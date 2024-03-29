
namespace SmartSalon.Application.Abstractions;

public interface ICurrentUserAccessor : IScopedLifetime
{
    Id? Id { get; }
}
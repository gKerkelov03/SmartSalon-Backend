
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface ICurrentUserAccessor : IScopedLifetime
{
    Id? Id { get; }
}
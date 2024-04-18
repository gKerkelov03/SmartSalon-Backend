
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface ICurrentUserAccessor : IScopedLifetime
{
    Id Id { get; }
    bool IsAdmin { get; }
    bool IsOwner { get; }
    bool IsCustomer { get; }
    bool IsWorker { get; }
}
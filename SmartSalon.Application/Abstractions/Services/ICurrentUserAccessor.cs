
using SmartSalon.Application.Abstractions.Lifetimes;

namespace SmartSalon.Application.Abstractions;

public interface ICurrentUserAccessor : IScopedLifetime
{
    Id? Id { get; }
}
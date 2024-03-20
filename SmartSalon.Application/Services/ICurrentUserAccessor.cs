
using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Services;

public interface ICurrentUserAccessor : IScopedLifetime
{
    Id Id { get; }
}
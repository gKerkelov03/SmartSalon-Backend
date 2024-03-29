using SmartSalon.Application.Abstractions;

namespace SmartSalon.Application.Services;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    public Id Id { get; }
}
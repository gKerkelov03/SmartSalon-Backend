
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface IJwtTokensGenerator : IScopedLifetime
{
    string GenerateJwt(Id userId, IEnumerable<string> roles);
}
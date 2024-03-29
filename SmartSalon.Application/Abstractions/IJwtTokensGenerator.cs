
namespace SmartSalon.Application.Abstractions;

public interface IJwtTokensGenerator : IScopedLifetime
{
    string GenerateFor(Id userId);
}
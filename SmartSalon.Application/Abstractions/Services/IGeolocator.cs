
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Models;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Abstractions;

public interface IGeolocator : IScopedLifetime
{
    public Task<Result<Coordinates>> GetCoordinatesAsync(string address);
}
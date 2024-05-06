using SmartSalon.Application.Abstractions.Lifetime;

public interface IIncreasingNumbersProvider : ISingletonLifetime
{
    public int Next { get; }


}
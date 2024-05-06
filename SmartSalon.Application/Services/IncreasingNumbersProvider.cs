
namespace SmartSalon.Application.Services;

public class IncreasingNumbersProvider : IIncreasingNumbersProvider
{
    private int _current = 0;
    public int Next => ++_current;
}


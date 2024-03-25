using System.Collections;

namespace SmartSalon.Shared.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<TElement>(
        this IEnumerable<TElement> enumerable,
        Action<TElement> action
    )
    {
        foreach (var element in enumerable)
        {
            action(element);
        }
    }

    public static void ForEach(
        this IEnumerable enumerable,
        Action<object> action
    )
    {
        foreach (var element in enumerable)
        {
            action(element);
        }
    }
}

namespace SmartSalon.Application.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<TElement>(this IEnumerable<TElement> enumerable, Action<TElement> action)
    {
        foreach (var element in enumerable)
        {
            action(element);
        }
    }

    public static void ForEach<TElement>(this IEnumerable<TElement> enumerable, Action<TElement, int> action)
    {
        var index = 0;

        foreach (var element in enumerable)
        {
            action(element, index);
            index++;
        }
    }
}

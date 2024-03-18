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

    public static async Task ForEachAsync<TElement>(
        this IEnumerable<TElement> enumerable,
        Func<TElement, Task> action
    )
    {
        var tasks = new List<Task>();

        foreach (var element in enumerable)
        {
            tasks.Add(action(element));
        }

        await Task.WhenAll(tasks);
    }
}

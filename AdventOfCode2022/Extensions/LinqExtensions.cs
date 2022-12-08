namespace AdventOfCode2022.Extensions;

internal static class LinqExtensions
{
    public static IEnumerable<IEnumerable<TSource>> Split<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        List<TSource> group = new List<TSource>();
        foreach (TSource item in source)
        {
            if (predicate(item))
            {
                yield return group.AsEnumerable();
                group = new List<TSource>();
            }
            else
            {
                group.Add(item);
            }
        }
        yield return group.AsEnumerable();
    }

    public static IEnumerable<IEnumerable<TSource>> SplitInHalf<TSource>(this IEnumerable<TSource> source)
    {
        List<TSource> group = new();
        group.AddRange(source.Take(source.Count() / 2));
        yield return group.AsEnumerable();

        group = new List<TSource>();
        group.AddRange(source.Skip(source.Count() / 2));
        yield return group.AsEnumerable();
    }

    public static List<List<TSource>> ChunkBy<TSource>(this IEnumerable<TSource> source, int chunkSize)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }

    public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
    {
        return !b.Except(a).Any();
    }

}

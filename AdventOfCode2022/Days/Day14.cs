namespace AdventOfCode2022.Days;

public class Day14 : IDay
{
    public string Part1(List<string> inputs)
    {
        var points = inputs
            .Select(x => x
                .Split("->", StringSplitOptions.TrimEntries)
                .Select(p => p.Split(',', 2))
                .Select(p => (int.Parse(p[0]), int.Parse(p[1])))
                .ToList())
            .ToList();

        var walls = (from path in points
                     from p in EnumPointsInPath(path)
                     select p).ToHashSet();

        var start = (500, 0);
        var bottom = walls.Select(p => p.Item2).Max();

        var result = PourSand(start, bottom, walls.Contains);
        return result.Count.ToString();
    }

    public string Part2(List<string> inputs)
    {
        var points = inputs
        .Select(x => x
            .Split("->", StringSplitOptions.TrimEntries)
            .Select(p => p.Split(',', 2))
            .Select(p => (int.Parse(p[0]), int.Parse(p[1])))
            .ToList())
        .ToList();

        var walls = (from path in points
                     from p in EnumPointsInPath(path)
                     select p).ToHashSet();

        var start = (500, 0);
        var bottom = walls.Select(p => p.Item2).Max();

        var floor = bottom + 2;
        var result = PourSand(start, int.MaxValue, p => p.Item2 == floor || walls.Contains(p));
        return result.Count.ToString();
    }

    static HashSet<(int, int)> PourSand((int, int) start, int bottom, Func<(int, int), bool> wall)
    {
        HashSet<(int, int)> sand = new();

        while (true)
        {
            var sandPos = PlaceSandUnit(start, bottom, p => wall(p) || sand.Contains(p));
            if (!sandPos.HasValue)
                break;

            sand.Add(sandPos.Value);
        }

        return sand;
    }

    static (int, int)? PlaceSandUnit((int, int) start, int bottom, Func<(int, int), bool> blocked)
    {
        if (blocked(start))
            return null;

        var pos = start;
        while (pos.Item2 <= bottom)
        {
            var next = EnumNextPositions(pos).Where(p => !blocked(p)).FirstOrDefault();
            if (next == default)
                return pos;

            pos = next;
        }

        return null;
    }

    static IEnumerable<(int, int)> EnumNextPositions((int, int) point)
    {
        var newY = point.Item2 + 1;
        yield return (point.Item1, newY);
        yield return (point.Item1 - 1, newY);
        yield return (point.Item1 + 1, newY);
    }

    static IEnumerable<(int, int)> EnumPointsInPath(IEnumerable<(int, int)> path)
        => from pair in path.Zip(path.Skip(1))
            from p in EnumPointsInLine(pair.First, pair.Second)
            select p;

    static IEnumerable<(int, int)> EnumPointsInLine((int, int) start, (int, int) end)
    {
        if (start.Item1 == end.Item1)
        {
            var minY = Math.Min(start.Item2, end.Item2);
            var count = Math.Abs(end.Item2 - start.Item2) + 1;
            return Enumerable.Range(minY, count).Select(y => (start.Item1, y));
        }
        else if (start.Item2 == end.Item2)
        {
            var minX = Math.Min(start.Item1, end.Item1);
            var count = Math.Abs(end.Item1 - start.Item1) + 1;
            return Enumerable.Range(minX, count).Select(x => (x, start.Item2));
        }
        else
        {
            return Enumerable.Empty<(int, int)>();
        }
    }
}

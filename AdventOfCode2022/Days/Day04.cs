using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

public class Day04 : IDay
{
    public string Part1(List<string> inputs)
    {
        var sections = SplitInputsIntoRanges(inputs);
        var fullIntersections = sections
            .Select(x => x.First().ContainsAllItems(x.Last()) || x.Last().ContainsAllItems(x.First()));

        return fullIntersections.Count(x => x).ToString();
    }

    public string Part2(List<string> inputs)
    {
        var sections = SplitInputsIntoRanges(inputs);
        var intersections = sections
            .Select(x => x.First().Intersect(x.Last()).Any() || x.First().Intersect(x.Last()).Any());

        return intersections.Count(x => x).ToString();
    }

    private static IEnumerable<IEnumerable<IEnumerable<int>>> SplitInputsIntoRanges(List<string> inputs)
    {
        return inputs
            .Select(x => x.Split(','))
            .Select(x => x.Select(y => y.Split('-').Select(int.Parse))
                        .Select(y => Enumerable.Range(y.First(), y.Last() - y.First() + 1)));
    }
}

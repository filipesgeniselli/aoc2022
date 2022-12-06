using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

internal class Day01 : IDay
{
    //70369
    //203002
    public void Part1(List<string> inputs)
    {
        var result = inputs.Split(x => string.IsNullOrEmpty(x)).Select(x => x.Select(int.Parse).Sum()).Max();
        Console.WriteLine(result);
    }

    public void Part2(List<string> inputs)
    {
        var result = inputs.Split(x => string.IsNullOrEmpty(x)).Select(x => x.Select(int.Parse).Sum());
        var sum = result.OrderByDescending(x => x).Take(3).Skip(0).Sum();
        Console.WriteLine(sum);
    }
}

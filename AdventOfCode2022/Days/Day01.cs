using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

public class Day01 : IDay
{
    //70369
    //203002
    public string Part1(List<string> inputs)
    {
        return inputs.Split(x => string.IsNullOrEmpty(x)).Select(x => x.Select(int.Parse).Sum()).Max().ToString();
    }

    public string Part2(List<string> inputs)
    {
        var result = inputs.Split(x => string.IsNullOrEmpty(x)).Select(x => x.Select(int.Parse).Sum());
        return result.OrderByDescending(x => x).Take(3).Skip(0).Sum().ToString();
    }
}

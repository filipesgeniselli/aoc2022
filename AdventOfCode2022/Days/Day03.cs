using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

public class Day03 : IDay
{
    public string Part1(List<string> inputs)
    {
        var rucksacks = inputs
            .Select(x => x.ToCharArray()
                        .Select(x => ToPriority(x))
                        .SplitInHalf()
                        .ToList())
            .ToList();
        var intersections = rucksacks
            .Select(x => x[0].Intersect(x[1])
                        .First())
            .ToList();
        return intersections.Sum().ToString();
    }

    public string Part2(List<string> inputs)
    {
        var elfGroup = inputs
            .Select(x => x.ToCharArray()
                        .Select(x => ToPriority(x))
                        .ToList())
            .ChunkBy(3);
        var intersections = elfGroup
            .Select(x => x[0].Intersect(x[1])
                        .Intersect(x[2])
                        .First())
            .ToList();
        return intersections.Sum().ToString();
    }

    private static int ToPriority(char x)
    {
        return x switch
        {
            'a' => 1,
            'b' => 2,
            'c' => 3,
            'd' => 4,
            'e' => 5,
            'f' => 6,
            'g' => 7,
            'h' => 8,
            'i' => 9,
            'j' => 10,
            'k' => 11,
            'l' => 12,
            'm' => 13,
            'n' => 14,
            'o' => 15,
            'p' => 16,
            'q' => 17,
            'r' => 18,
            's' => 19,
            't' => 20,
            'u' => 21,
            'v' => 22,
            'w' => 23,
            'x' => 24,
            'y' => 25,
            'z' => 26,
            'A' => 27,
            'B' => 28,
            'C' => 29,
            'D' => 30,
            'E' => 31,
            'F' => 32,
            'G' => 33,
            'H' => 34,
            'I' => 35,
            'J' => 36,
            'K' => 37,
            'L' => 38,
            'M' => 39,
            'N' => 40,
            'O' => 41,
            'P' => 42,
            'Q' => 43,
            'R' => 44,
            'S' => 45,
            'T' => 46,
            'U' => 47,
            'V' => 48,
            'W' => 49,
            'X' => 50,
            'Y' => 51,
            'Z' => 52,
            _ => 0
        };
    }
}

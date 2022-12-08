namespace AdventOfCode2022.Days;

public class Day06 : IDay
{
    public string Part1(List<string> inputs)
    {
        var stream = inputs[0];

        for (int i = 0; i < stream.Length; i++)
        {
            var marker = stream.Skip(i).Take(4).Distinct().Count();
            if(marker == 4)
            {
                return (i + 4).ToString();
            }
        }
        return string.Empty;
    }

    public string Part2(List<string> inputs)
    {
        var stream = inputs[0];

        for (int i = 0; i < stream.Length; i++)
        {
            var marker = stream.Skip(i).Take(14).Distinct().Count();
            if (marker == 14)
            {
                return (i + 14).ToString();
            }
        }
        return string.Empty;
    }
}

namespace AdventOfCode2022.Days;

internal class Day06 : IDay
{
    public void Part1(List<string> inputs)
    {
        var stream = inputs[0];

        for (int i = 0; i < stream.Length; i++)
        {
            var marker = stream.Skip(i).Take(4).Distinct().Count();
            if(marker == 4)
            {
                Console.WriteLine(i + 4);
                break;
            }
        }
    }

    public void Part2(List<string> inputs)
    {
        var stream = inputs[0];

        for (int i = 0; i < stream.Length; i++)
        {
            var marker = stream.Skip(i).Take(14).Distinct().Count();
            if (marker == 14)
            {
                Console.WriteLine(i + 14);
                break;
            }
        }
    }
}

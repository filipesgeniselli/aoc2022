using AdventOfCode2022.Days;
using FluentAssertions;
using System.Xml.XPath;

namespace AdventOfCode2022.Tests.DaysTest;

public class DaysTests
{
    [Theory]
    [InlineData(1, "24000")]
    [InlineData(2, "15")]
    [InlineData(3, "157")]
    [InlineData(4, "2")]
    [InlineData(5, "CMZ")]
    [InlineData(6, "7")]
    [InlineData(7, "95437")]
    [InlineData(8, "21")]
    [InlineData(9, "88")]
    [InlineData(10, "13140")]
    [InlineData(11, "10605")]
    public async Task TestDayPart1(int day, string result)
    {
        CurrentDay currentDayExecution = new (day);
        List<string> input = await FileManager.ReadFromFileAsync(day, true);

        string solution = currentDayExecution.Part1(input);

        solution.Should().Be(result);
    }

    [Theory]
    [InlineData(1, "45000")]
    [InlineData(2, "12")]
    [InlineData(3, "70")]
    [InlineData(4, "4")]
    [InlineData(5, "MCD")]
    [InlineData(6, "19")]
    [InlineData(7, "24933642")]
    [InlineData(8, "8")]
    [InlineData(9, "36")]
    [InlineData(10, $"##..##..##..##..##..##..##..##..##..##..|" +
                    $"###...###...###...###...###...###...###.|" +
                    $"####....####....####....####....####....|" +
                    $"#####.....#####.....#####.....#####.....|" +
                    $"######......######......######......####|" +
                    $"#######.......#######.......#######.....")]
    [InlineData(11, "2713310158")]
    public async Task TestDayPart2(int day, string result)
    {
        result = result.Replace("|", Environment.NewLine);
        CurrentDay currentDayExecution = new (day);
        List<string> input = await FileManager.ReadFromFileAsync(day, true);

        string solution = currentDayExecution.Part2(input);

        solution.Should().Be(result);
    }

}

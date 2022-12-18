using FluentAssertions;

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
    [InlineData(12, "31")]
    [InlineData(13, "13")]
    [InlineData(14, "24")]
    [InlineData(15, "4582667")]
    [InlineData(16, "1651")]
    [InlineData(17, "3068")]
    [InlineData(18, "64")]
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
    [InlineData(12, "29")]
    [InlineData(13, "140")]
    [InlineData(14, "93")]
    [InlineData(15, "10961118625406")]
    [InlineData(16, "1707")]
    [InlineData(17, "1514285714288")]
    [InlineData(18, "58")]
    public async Task TestDayPart2(int day, string result)
    {
        result = result.Replace("|", Environment.NewLine);
        CurrentDay currentDayExecution = new (day);
        List<string> input = await FileManager.ReadFromFileAsync(day, true);

        string solution = currentDayExecution.Part2(input);

        solution.Should().Be(result);
    }

}

using AdventOfCode2022;
using AdventOfCode2022.Days;

var currentDay = new CurrentDay();

async Task Part1Async(bool test = true)
{
    Console.WriteLine($"Executing day {currentDay.Day}");
    var inputs = await FileManager.ReadFromFileAsync(currentDay.Day, test);
    currentDay.Part1(inputs);
}

async Task Part2Async(bool test = true)
{
    var inputs = await FileManager.ReadFromFileAsync(currentDay.Day, test);
    currentDay.Part2(inputs);
}


await Part1Async(false);
await Part2Async(false);
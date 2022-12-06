using AdventOfCode2022;

var currentDay = new CurrentDay();
var isTestExecution = false;

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

await Part1Async(isTestExecution);
await Part2Async(isTestExecution);
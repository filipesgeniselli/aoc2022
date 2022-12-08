using AdventOfCode2022;

var currentDay = new CurrentDay();
var isTestExecution = false;

async Task Part1Async(bool test = true)
{    
    var inputs = await FileManager.ReadFromFileAsync(currentDay.Day, test);
    Console.WriteLine(currentDay.Part1(inputs));
}

async Task Part2Async(bool test = true)
{
    var inputs = await FileManager.ReadFromFileAsync(currentDay.Day, test);
    Console.WriteLine(currentDay.Part2(inputs));
}

Console.WriteLine($"Executing day {currentDay.Day}");
await Part1Async(isTestExecution);
await Part2Async(isTestExecution);
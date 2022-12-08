namespace AdventOfCode2022;

public class CurrentDay
{
    public int Day { get; init; }

    public CurrentDay()
    {
        Day = DateTime.Today.Day;
    }
    public CurrentDay(int day)
    {
        Day = day;
    }

    public string Part1(List<string> inputs)
    {
        var currentDayInstance = GetInstance();

        if (currentDayInstance == null) { return string.Empty; }

        return currentDayInstance.Part1(inputs);
    }

    public string Part2(List<string> inputs)
    {
        var currentDayInstance = GetInstance();

        if (currentDayInstance == null) { return string.Empty; }

        return currentDayInstance.Part2(inputs);
    }

    private IDay? GetInstance()
    {
        var type = Type.GetType($"AdventOfCode2022.Days.Day{Day:00}");
        if (type == null)
            return null;

        return Activator.CreateInstance(type) as IDay;
    }
}

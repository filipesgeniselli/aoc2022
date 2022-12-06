namespace AdventOfCode2022;

internal class CurrentDay
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

    public void Part1(List<string> inputs)
    {
        var currentDayInstance = GetInstance();

        if (currentDayInstance == null) { return; }

        currentDayInstance.Part1(inputs);
    }

    public void Part2(List<string> inputs)
    {
        var currentDayInstance = GetInstance();

        if (currentDayInstance == null) { return; }

        currentDayInstance.Part2(inputs);
    }

    private IDay? GetInstance()
    {
        var type = Type.GetType($"AdventOfCode2022.Days.Day{Day:00}");
        if (type == null)
            return null;

        return Activator.CreateInstance(type) as IDay;
    }
}

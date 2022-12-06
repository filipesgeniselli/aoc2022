using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

internal class Day05 : IDay
{
    public void Part1(List<string> inputs)
    {
        var challenge = inputs.Split(x => string.IsNullOrEmpty(x));
        var crateStack = challenge.Take(1).Single().ToList();
        var movements = challenge.Skip(1).Single();

        var listStack = CreateCrateStack(crateStack);
        foreach (var movement in movements)
        {
            var directions = movement.Split(' ');
            _ = int.TryParse(directions[1], out int crateAmount);
            _ = int.TryParse(directions[3], out int origin);
            _ = int.TryParse(directions[5], out int destination);

            for(int i = 0; i < crateAmount; i++) 
            {
                listStack[origin - 1].TryPop(out string crate);
                listStack[destination - 1].Push(crate);
            }
        }

        Console.WriteLine(string.Join(string.Empty, listStack.Select(x => x.Peek()[1])));
    }

    public void Part2(List<string> inputs)
    {
        var challenge = inputs.Split(x => string.IsNullOrEmpty(x));
        var crateStack = challenge.Take(1).Single().ToList();
        var movements = challenge.Skip(1).Single();

        var listStack = CreateCrateStack(crateStack);

        foreach (var movement in movements)
        {
            List<string> crateList = new();
            var directions = movement.Split(' ');
            _ = int.TryParse(directions[1], out int crateAmount);
            _ = int.TryParse(directions[3], out int origin);
            _ = int.TryParse(directions[5], out int destination);

            for (int i = 0; i < crateAmount; i++)
            {
                listStack[origin - 1].TryPop(out string crate);
                crateList.Add(crate);
            }
            
            crateList.Reverse();

            foreach (var crate in crateList)
            {
                listStack[destination - 1].Push(crate);
            }
        }
        Console.WriteLine(string.Join(string.Empty, listStack.Select(x => x.Peek()[1])));
    }

    private List<Stack<string>> CreateCrateStack(List<string> stackConfiguration)
    {
        List<Stack<string>> listStack = new();
        for (int i = stackConfiguration.Count - 2; i >= 0; i--)
        {
            var input = stackConfiguration[i];
            int currentColumn = 0;

            for (int j = 0; j < input.Length; j += 4)
            {
                var crate = input.Substring(j, 3);
                if (crate == "   ")
                {
                    currentColumn++;
                    continue;
                }

                if (listStack.Count <= currentColumn)
                {
                    listStack.Add(new Stack<string>());
                }

                listStack[currentColumn].Push(crate);
                currentColumn++;
            }
        }

        return listStack;
    }
}

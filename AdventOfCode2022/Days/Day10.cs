using System.Text;

namespace AdventOfCode2022.Days;

public class Day10 : IDay
{
    public string Part1(List<string> inputs)
    {
        List<int> signalStrength = new();

        int cycle = 1;
        int xValue = 1;

        foreach (var input in inputs.Select(x => x.Split(' ')))
        {
            var command = input[0];
            if(command == "noop")
            {
                cycle++;
                CheckExpectedCycle(cycle, xValue, signalStrength);
            }

            if(command == "addx")
            {
                cycle++;
                CheckExpectedCycle(cycle, xValue, signalStrength);
                
                _ = int.TryParse(input[1], out int x);
                xValue += x;
                
                cycle++;
                CheckExpectedCycle(cycle, xValue, signalStrength);
            }
        }

        return signalStrength.Sum().ToString();
    }

    public string Part2(List<string> inputs)
    {
        List<StringBuilder> pixels = new() { new() };
        var commands = inputs.Select(x => x.Split(' '));

        int cycle = 0;
        int pixelPos = 0;
        int xValue = 1;
        foreach (var command in commands)
        {
            if (command[0] == "noop")
            {
                DrawPixel(pixelPos, xValue, pixels);
                pixelPos++;

                cycle++;
                if (IsExpectedCycleDraw(cycle))
                {
                    pixels.Add(new());
                    pixelPos = 0;
                }
            }

            if (command[0] == "addx")
            {
                DrawPixel(pixelPos, xValue, pixels);
                pixelPos++;
                
                cycle++;
                if (IsExpectedCycleDraw(cycle))
                {
                    pixels.Add(new());
                    pixelPos = 0;
                }

                DrawPixel(pixelPos, xValue, pixels);
                pixelPos++;

                _ = int.TryParse(command[1], out int x);
                xValue += x;
                cycle++;
                if (IsExpectedCycleDraw(cycle))
                {
                    pixels.Add(new());
                    pixelPos = 0;
                }
            }
        }

        return string.Join(Environment.NewLine, pixels.Take(6).Select(x => x.ToString()));
    }

    private static void DrawPixel(int pixelPos, int xValue, List<StringBuilder> pixels)
    {
        bool shouldDrawLitPixel = (pixelPos == xValue || pixelPos == xValue + 1 || pixelPos == xValue - 1);
        pixels.Last().Append(shouldDrawLitPixel ? "#" : ".");
    }

    private static void CheckExpectedCycle(int cycle, int xValue, List<int> signalStrength)
    {
        int[] validCycles = new int[] { 20, 60, 100, 140, 180, 220 };
        if (validCycles.Contains(cycle))
        {
            signalStrength.Add(xValue * cycle);
        }
    }

    private static bool IsExpectedCycleDraw(int cycle)
    {
        int[] validCycles = new int[] { 40, 80, 120, 160, 200, 240 };
        return validCycles.Contains(cycle);
    }
}

namespace AdventOfCode2022.Days;

public class Day16 : IDay
{
    Dictionary<string, int> valves;
    Dictionary<string, List<string>> tunnels;
    Dictionary<(int, string, int), int> cache = new();
    Dictionary<string, Dictionary<string, int>> dists = new();
    Dictionary<string, int> indices = new();
    
    public string Part1(List<string> inputs)
    {
        ParseInputs(inputs);

        int b = CalculateInitialBitwise();
        int result = 0;

        foreach (var i in Enumerable.Range(0, b + 1))
        {
            result = Math.Max(result, MaxValue(30, "AA", i));
        }

        return result.ToString();
    }

    public string Part2(List<string> inputs)
    {
        ParseInputs(inputs);

        int b = CalculateInitialBitwise();
        int result = 0;

        foreach (var i in Enumerable.Range(0, (b + 1) / 2))
        {
            result = Math.Max(result, MaxValue(26, "AA", i) + MaxValue(26, "AA", b ^ i));
        }

        return result.ToString();
    }

    private void ParseInputs(List<string> inputs)
    {
        var newInputs = inputs.Select(x => x.Trim()
                            .Replace("Valve ", "")
                            .Replace(" has flow rate=", ";")
                            .Replace(" tunnel leads to valve ", "")
                            .Replace(" tunnels lead to valves ", "")
                            .Split(";")
                            );

        valves = newInputs.Select(x => new { Key = x[0], Rate = int.Parse(x[1]) }).ToDictionary(x => x.Key, x => x.Rate);
        tunnels = newInputs.Select(x => new { Key = x[0], Tunnels = x[2].Split(", ").ToList() }).ToDictionary(x => x.Key, x => x.Tunnels);
        cache = new();
        dists = new();
        indices = new();
    }

    private int CalculateInitialBitwise()
    {
        List<string> nonEmpty = new();

        foreach (var (valve, rate) in valves)
        {
            if (valve != "AA" && rate == 0)
                continue;

            if (valve != "AA")
                nonEmpty.Add(valve);

            var queue = new Queue<(int, string)>();
            queue.Enqueue((0, valve));

            HashSet<string> visited = new() { valve };

            while (queue.Count > 0)
            {
                var (distance, position) = queue.Dequeue();
                foreach (var neighbor in tunnels[position])
                {
                    if (visited.Contains(neighbor))
                        continue;

                    visited.Add(neighbor);
                    if (valves[neighbor] > 0)
                    {
                        if (dists.ContainsKey(valve) && dists[valve].ContainsKey(neighbor))
                        {
                            dists[valve][neighbor] = distance + 1;
                        }
                        else
                        {
                            if (dists.ContainsKey(valve))
                                dists[valve].Add(neighbor, distance + 1);
                            else
                                dists.Add(valve, new() { { neighbor, distance + 1 } });
                        }
                    }
                    queue.Enqueue((distance + 1, neighbor));
                }
            }
        }

        foreach (var (element, index) in nonEmpty.Select((item, index) => (item, index)))
            indices.Add(element, index);

        return (1 << nonEmpty.Count) - 1;
    }

    private int MaxValue(int time, string valve, int bitmask)
    {
        if (cache.ContainsKey((time, valve, bitmask)))
        {
            return cache[(time, valve, bitmask)];
        }

        int maxVal = 0;
        foreach (var (neighbor, values) in dists[valve])
        {
            var bit = 1 << indices[neighbor];
            if ((bitmask & bit) > 0)
            {
                continue;
            }

            var remainingTime = time - dists[valve][neighbor] - 1;
            if (remainingTime <= 0)
            {
                continue;
            }
            maxVal = Math.Max(maxVal, MaxValue(remainingTime, neighbor, bitmask | bit) + valves[neighbor] * remainingTime);
        }

        cache.Add((time, valve, bitmask), maxVal);
        return maxVal;
    }

}

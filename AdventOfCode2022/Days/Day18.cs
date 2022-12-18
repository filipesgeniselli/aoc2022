namespace AdventOfCode2022.Days;

public class Day18 : IDay
{
    HashSet<(int, int, int)> outSet = new();
    HashSet<(int, int, int)> inSet = new();
    List<(int, int, int)> points;

    public string Part1(List<string> inputs)
    {
        return Solve(inputs, false);
    }

    public string Part2(List<string> inputs)
    {
        return Solve(inputs, true);
    }

    private string Solve(List<string> inputs, bool isPart2 = false)
    {
        outSet = new();
        inSet = new();
        points = inputs.Select(x => (int.Parse(x.Split(',')[0]),
                                     int.Parse(x.Split(',')[1]),
                                     int.Parse(x.Split(',')[2]))).ToList();

        int result = 0;
        foreach (var (x, y, z) in points)
        {
            if (ReachesOutside(x + 1, y, z, isPart2)) result++;
            if (ReachesOutside(x - 1, y, z, isPart2)) result++;
            if (ReachesOutside(x, y + 1, z, isPart2)) result++;
            if (ReachesOutside(x, y - 1, z, isPart2)) result++;
            if (ReachesOutside(x, y, z + 1, isPart2)) result++;
            if (ReachesOutside(x, y, z - 1, isPart2)) result++;
        }

        return result.ToString();
    }

    private bool ReachesOutside(int x, int y, int z, bool isPart2 = false)
    {
        if(outSet.Contains((x,y,z)))
        {
            return true;
        }
        if (inSet.Contains((x, y, z)))
        {
            return false;
        }

        HashSet<(int, int, int)> seenSet = new();
        Queue<(int, int, int)> queue = new();
        queue.Enqueue((x, y, z));

        while(queue.Count > 0)
        {
            (x, y, z) = queue.Dequeue();
            if (points.Contains((x, y, z)))
            {
                continue;
            }
            if(seenSet.Contains((x, y, z)))
            {
                continue;
            }
            seenSet.Add((x, y, z));

            if(seenSet.Count > (isPart2 ? 5000 : 0))
            {
                foreach(var p in seenSet)
                {
                    outSet.Add(p);
                }
                return true;
            }

            queue.Enqueue((x + 1, y, z));
            queue.Enqueue((x - 1, y, z));
            queue.Enqueue((x, y + 1, z));
            queue.Enqueue((x, y - 1, z));
            queue.Enqueue((x, y, z + 1));
            queue.Enqueue((x, y, z - 1));
        }
        foreach (var p in seenSet)
        {
            inSet.Add(p);
        }

        return false;
    }
}

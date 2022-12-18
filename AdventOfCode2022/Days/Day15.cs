namespace AdventOfCode2022.Days;

public class Day15 : IDay
{
    public string Part1(List<string> inputs)
    {
        var lines = inputs.Select(x => x
            .Replace("x=", "")
            .Replace("y=", "")
            .Replace("Sensor at ", "")
            .Replace(" closest beacon is at", "")
            .Split(":")).ToList();

        int y = 2000000;
        HashSet<int> beaconXs = new();
        List<(int, int)> segments = new();

        foreach (var line in lines)
        {
            var ax = int.Parse(line[0].Split(',')[0]);
            var ay = int.Parse(line[0].Split(',')[1]);

            var bx = int.Parse(line[1].Split(',')[0]);
            var by = int.Parse(line[1].Split(',')[1]);

            if(by == y)
            {
                beaconXs.Add(bx);
            }
            var dist = Math.Abs(ax - bx) + Math.Abs(ay - by) - Math.Abs(ay - y);
            if (0 <= dist)
            {
                segments.Add((ax - dist, ax + dist + 1));
            }
        }

        segments.Sort();

        int c = 0;
        var end = segments[0].Item1;
        foreach (var s in segments)
        {
            var start = Math.Max(s.Item1, end);
            end = Math.Max(end, s.Item2);

            c += end - start - beaconXs.Where(x => start <= x && x < end).Count();
        }
        
        return c.ToString();
    }

    public string Part2(List<string> inputs)
    {
        var lines = inputs.Select(x => x
            .Replace("x=", "")
            .Replace("y=", "")
            .Replace("Sensor at ", "")
            .Replace(" closest beacon is at", "")
            .Split(":")).ToList();

        List<(int, int)> beacons = new();
        List<(int, int)> sensors = new();

        foreach (var line in lines)
        {
            var sensorPoints = line[0].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
            var beaconPoints = line[1].Split(',', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();

            sensors.Add((sensorPoints[0], sensorPoints[1]));
            beacons.Add((beaconPoints[0], beaconPoints[1]));
        }

        int yMin = 0;
        int yMax = 4000000;

        for(int y = yMin; y <= yMax; y++)
        {
            List<(int, int)> segments = new();
            foreach (var ((sX, sY), (bX, bY)) in sensors.Zip(beacons))
            {
                var dist = Math.Abs(sX - bX) + Math.Abs(sY - bY) - Math.Abs(sY - y);
                if (0 <= dist)
                {
                    segments.Add((sX - dist, sX + dist));
                }                    
            }

            segments.Sort();

            var end = yMin;
            foreach (var s in segments)
            {
                var x = end + 1;
                if (x < s.Item1 && x <= yMax && !beacons.Contains((x, y)))
                {
                    long result = (long)x * (long)yMax + y;
                    return result.ToString();
                }

                end = Math.Max(end, s.Item2);
            }
        }
        
        return string.Empty;
    }
}

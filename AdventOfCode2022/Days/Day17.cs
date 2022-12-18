namespace AdventOfCode2022.Days;

public class Day17 : IDay
{
    private const int TowerWidth = 7;
    List<List<(int, long)>> rockTypes = new()
    {
        new List<(int, long)>() { (2, 0), (3, 0), (4, 0), (5, 0) }, //-
        new List<(int, long)>() { (3, 0), (2, 1), (3, 1), (4, 1), (3, 2) }, // +
        new List<(int, long)>() { (2, 0), (3, 0), (4, 0), (4, 1), (4, 2) }, // _|
        new List<(int, long)>() { (2, 0), (2, 1), (2, 2), (2, 3) }, // |
        new List<(int, long)>() { (2, 0), (2, 1), (3, 0), (3, 1) } // square
    };

    public string Part1(List<string> inputs)
    {
        var jetDirections = inputs.First();
        var tower = new HashSet<(int, long)>()
        {
            (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0)
        };

        var result = RunSimulations(jetDirections, tower, 0, 0, 2022);
        return result.Select(x => x.Item2).Max().ToString();
    }

    public string Part2(List<string> inputs)
    {
        //1.000.000.000.000
        var jetDirections = inputs.First();
        var tower = new HashSet<(int, long)>()
        {
            (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0)
        };

        var result = RunSimulationsPredicting(jetDirections, tower, 0, 0, 1000000000000);
        return result.ToString();
    }

    private HashSet<(int, long)> RunSimulations(string jetDirections, HashSet<(int, long)> tower, long rockIndex, int jetIndex, long totalRocks)
    {
        while (rockIndex < totalRocks)
        {
            var rock = rockTypes[(int)(rockIndex % rockTypes.Count)];
            rockIndex++;

            var yOff = tower.Select(x => x.Item2).Max() + 4;

            rock = rock.Select(x => { x.Item2 += yOff; return x; }).ToList();

            while (true)
            {
                var jet = jetDirections[jetIndex % jetDirections.Length];
                int directionX = 1;
                if (jet == '<')
                    directionX = -1;

                jetIndex++;

                var newRock = rock.Select(x => { x.Item1 += directionX; return x; }).ToList();
                if (newRock.Any(x => x.Item1 < 0 || x.Item1 >= TowerWidth))
                {
                    newRock = rock;
                }
                else if (newRock.Any(x => tower.Contains(x)))
                {
                    newRock = rock;
                }
                rock = newRock;
                newRock = rock.Select(x => { x.Item2 -= 1; return x; }).ToList();
                if (newRock.Any(x => tower.Contains(x)))
                {
                    rock.ForEach(x => tower.Add(x));
                    break;
                }

                rock = newRock;
            }
        }

        return tower;
    }

    private long RunSimulationsPredicting(string jetDirections, HashSet<(int, long)> tower, long rockIndex, int jetIndex, long totalRocks)
    {
        var towerHeigths = new Dictionary<((int, int), int, int), List<(long, long, (long, long, long, long, long, long, long))>>();
        while (rockIndex < totalRocks)
        {
            var startKey = ((int)(rockIndex % rockTypes.Count), jetIndex % jetDirections.Length);

            var rock = rockTypes[(int)(rockIndex % rockTypes.Count)];
            rockIndex++;

            var yOff = tower.Select(x => x.Item2).Max() + 4;
            rock = rock.Select(x => { x.Item2 += yOff; return x; }).ToList();

            var moveX = 0;
            var moveY = 0;

            while (true)
            {
                var jet = jetDirections[jetIndex % jetDirections.Length];
                int directionX = 1;
                if (jet == '<')
                    directionX = -1;

                jetIndex++;

                var newRock = rock.Select(x => { x.Item1 += directionX; return x; }).ToList();
                if (newRock.Any(x => x.Item1 < 0 || x.Item1 >= TowerWidth))
                {
                    newRock = rock;
                }
                else if (newRock.Any(x => tower.Contains(x)))
                {
                    newRock = rock;
                }
                else
                {
                    moveX += directionX;
                }
                rock = newRock;

                newRock = rock.Select(x => { x.Item2 -= 1; return x; }).ToList();
                if (newRock.Any(x => tower.Contains(x)))
                {
                    rock.ForEach(x => tower.Add(x));
                    var key = (startKey, moveX, moveY);
                    var currentHeight = tower.Select(x => x.Item2).Max();

                    List<(long, long, (long, long, long, long, long, long, long))> history =
                        towerHeigths.GetValueOrDefault(key) ?? new();

                    var layout = (
                        tower.Where(x => x.Item1 == 0).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 1).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 2).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 3).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 4).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 5).Select(x => x.Item2 - currentHeight).Max(),
                        tower.Where(x => x.Item1 == 6).Select(x => x.Item2 - currentHeight).Max());
                    var stats = (currentHeight, rockIndex - 1, layout);

                    if (history.Count > 1)
                    {
                        var lastDiff = history.Last().Item1 - history.SkipLast(1).Last().Item1;
                        var currDiff = currentHeight - history.Last().Item1;

                        var lastMoveDiff = history.Last().Item2 - history.SkipLast(1).Last().Item2;
                        var currMoveDiff = (rockIndex - 1) - history.Last().Item2;

                        if (currDiff == lastDiff && lastMoveDiff == currMoveDiff)
                        {
                            var remainingTimes = totalRocks - rockIndex - 2;

                            var cycleLength = (rockIndex - 1) - history.Last().Item2;
                            long cycles = remainingTimes / cycleLength;
                            var totalJumpAhead = cycles * cycleLength;

                            var jumpedTower = RunSimulations(jetDirections, tower, rockIndex + totalJumpAhead, jetIndex, totalRocks);
                            return jumpedTower.Select(x => x.Item2).Max() + currDiff * cycles;
                        }
                    }

                    history.Add(stats);
                    if (towerHeigths.ContainsKey(key))
                    {
                        towerHeigths[key] = history;
                    }
                    else
                    {
                        towerHeigths.Add(key, history);
                    }

                    break;
                }

                moveY -= 1;
                rock = newRock;
            }
        }

        return 0;
    }
}
namespace AdventOfCode2022.Days;

public class Day09 : IDay
{
    public string Part1(List<string> inputs)
    {
        var headPosition = new Position(0, 0);
        var tailPosition = new Position(0, 0);

        foreach (var input in inputs.Select(x => x.Split(' ')))
        {
            var direction = input[0];
            _ = int.TryParse(input[1], out int count);

            int directionX = 0;
            int directionY = 0;

            switch (direction)
            {
                case "R":
                    directionX = 1;
                    break;
                case "L":
                    directionX = -1;
                    break;
                case "U":
                    directionY = 1;
                    break;
                case "D":
                    directionY = -1;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < count; i++)
            {
                headPosition.Move(directionX, directionY);
                if (!tailPosition.IsTouching(headPosition))
                {
                    tailPosition.MoveToTouch(headPosition);
                }
            }
        }
        tailPosition.History.Add(tailPosition);

        return tailPosition.History.Distinct().Count().ToString();
    }

    public string Part2(List<string> inputs)
    {
        var headPosition = new Position(0, 0);
        var tailPositions = new List<Position>
        {
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0),
            new Position(0,0)
        };

        foreach (var input in inputs.Select(x => x.Split(' ')))
        {
            var direction = input[0];
            _ = int.TryParse(input[1], out int count);

            int directionX = 0;
            int directionY = 0;

            switch (direction)
            {
                case "R":
                    directionX = 1;
                    break;
                case "L":
                    directionX = -1;
                    break;
                case "U":
                    directionY = 1;
                    break;
                case "D":
                    directionY = -1;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < count; i++)
            {
                headPosition.Move(directionX, directionY);
                for (int j = 0; j < tailPositions.Count; j++)
                {
                    if(!tailPositions[j].IsTouching(j == 0 ? headPosition : tailPositions[j - 1]))
                    {
                        tailPositions[j].MoveToTouch(j == 0 ? headPosition : tailPositions[j - 1]);
                    }                    
                }
            }
        }

        return (tailPositions.Last().History.Distinct().Count() + 1).ToString();
    }
}

internal class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public List<Position> History { get; private set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
        History = new();
    }

    public void Move(int directionX, int directionY)
    {
        History.Add(new Position(X, Y));
        X += directionX;
        Y += directionY;
    }

    public void MoveToTouch(Position refPosition)
    {
        if (X == refPosition.X)
        {
            if (Y < refPosition.Y) Move(0, 1);
            else Move(0, -1);
        }
        else if(Y == refPosition.Y)
        {
            if (X < refPosition.X) Move(1, 0);
            else Move(-1, 0);
        }
        else
        {
            if (X < refPosition.X && Y < refPosition.Y) Move(1, 1);
            if (X < refPosition.X && Y > refPosition.Y) Move(1, -1);
            if (X > refPosition.X && Y < refPosition.Y) Move(-1, 1);
            if (X > refPosition.X && Y > refPosition.Y) Move(-1, -1);
        }

        if (!IsTouching(refPosition))
        {
            throw new InvalidOperationException("Expected to be touching the refPosition now");
        }
    }

    public bool IsTouching(Position refPosition)
    {
        return
            (X == refPosition.X && Y + 1 == refPosition.Y) ||
            (X == refPosition.X && Y == refPosition.Y) ||            
            (X == refPosition.X && Y - 1 == refPosition.Y) ||

            (X + 1 == refPosition.X && Y + 1 == refPosition.Y) ||
            (X + 1 == refPosition.X && Y == refPosition.Y) ||            
            (X + 1 == refPosition.X && Y - 1 == refPosition.Y) ||

            (X - 1 == refPosition.X && Y + 1 == refPosition.Y) ||
            (X - 1 == refPosition.X && Y == refPosition.Y) ||
            (X - 1 == refPosition.X && Y - 1 == refPosition.Y);
    }

    public override bool Equals(object? obj)
    {
        return obj is Position position &&
               X == position.X &&
               Y == position.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

namespace AdventOfCode2022.Days;

public class Day08 : IDay
{
    private List<List<int>> TreeGrid = new();
    private int RowCount => TreeGrid.Count;
    private int ColumnCount => TreeGrid[0].Count;

    public string Part1(List<string> inputs)
    {
        TreeGrid = inputs.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToList()).ToList();
        int visibleCount = 0;

        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (i - 1 < 0 || j - 1 < 0 || i + 1 == RowCount || j + 1 == ColumnCount)
                {
                    visibleCount++;
                    continue; 
                }

                if (IsVisibleUntilEdge(i, j, 1, 0) || IsVisibleUntilEdge(i, j, -1, 0) ||
                    IsVisibleUntilEdge(i, j, 0, 1) || IsVisibleUntilEdge(i, j, 0, -1))
                {
                    visibleCount++;
                }                
            }
        }

        return visibleCount.ToString();
    }

    public string Part2(List<string> inputs)
    {
        TreeGrid = inputs.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToList()).ToList();
        int maxScenicScore = 0;

        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (i - 1 < 0 || j - 1 < 0 || i + 1 == RowCount || j + 1 == ColumnCount)
                {
                    continue;
                }

                int scenicScore = VisibilityUntilEdge(i, j, 1, 0) * VisibilityUntilEdge(i, j, -1, 0) *
                                  VisibilityUntilEdge(i, j, 0, 1) * VisibilityUntilEdge(i, j, 0, -1);
                if (scenicScore > maxScenicScore )
                {
                    maxScenicScore = scenicScore;
                }
            }
        }
        return maxScenicScore.ToString();
    }

    private bool IsVisibleUntilEdge(int x, int y, int directionX, int directionY)
    {
        var treeHeight = TreeGrid[x][y];

        bool isVisible = true;
        while (isVisible)
        {
            x += directionX;
            y += directionY;

            if (x < 0 || y < 0 || x == RowCount || y == ColumnCount)
                break;

            isVisible = treeHeight > TreeGrid[x][y];
        }

        return isVisible;
    }

    private int VisibilityUntilEdge(int x, int y, int directionX, int directionY)
    {
        var treeHeight = TreeGrid[x][y];

        int visibility = 0;
        bool isVisible = true;
        while (isVisible)
        {
            x += directionX;
            y += directionY;

            if (x < 0 || y < 0 || x == RowCount || y == ColumnCount)
                break;

            isVisible = treeHeight > TreeGrid[x][y];
            visibility++;
        }

        return visibility;
    }

}

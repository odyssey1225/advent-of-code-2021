namespace Day01;

public static class LavaTubes
{
    public static int GetLowPointRiskLevels(int[,] points)
    {
        return points
            .GetLowPoints()
            .Select(s => points[s.Item1, s.Item2] + 1)
            .Sum();
    }

    public static int GetLargestBasins(int[,] points)
    {
        return points
            .GetLowPoints()
            .Select(points.GetBasinSize)
            .OrderByDescending(o => o)
            .Take(3)
            .Aggregate(1, (current, next) => current * next);
    }

    private static IEnumerable<Tuple<int, int>> GetLowPoints(this int[,] points)
    {
        var lowPoints = new List<Tuple<int, int>>();
        var height = points.GetLength(0);
        var width = points.GetLength(1);
        
        for(var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (points.IsLowPoint(x, y, height, width))
                {
                    lowPoints.Add(new Tuple<int, int>(y, x));
                }
            }
        }

        return lowPoints;
    }

    private static bool IsLowPoint(this int[,] points, int x, int y, int height, int width)
    {
        return (x == 0 || points[y, x] < points[y, x - 1])
               && (x == width - 1 || points[y, x] < points[y, x + 1])
               && (y == 0 || points[y, x] < points[y - 1, x])
               && (y == height - 1 || points[y, x] < points[y + 1, x]);
    }

    private static int GetBasinSize(this int[,] points, Tuple<int, int> lowPoint)
    {
        var pointsInBasin = new List<Tuple<int, int>> { lowPoint };
        var pointsToVisit = new List<Tuple<int, int>> { lowPoint };
        var height = points.GetLength(0);
        var width = points.GetLength(1);

        while (pointsToVisit.Count > 0)
        {
            var newPoints = new List<Tuple<int, int>>();
            foreach (var point in pointsToVisit)
            {
                var (y, x) = point;
                
                var leftNeighbor = new Tuple<int, int>(y, x - 1);
                if (x > 0 && points[y, x - 1] < 9 && !pointsInBasin.Contains(leftNeighbor))
                {
                    pointsInBasin.Add(leftNeighbor);
                    newPoints.Add(leftNeighbor);
                }
                
                var rightNeighbor = new Tuple<int,int>(y, x + 1);
                if (x < width - 1 && points[y, x + 1] < 9 && !pointsInBasin.Contains(rightNeighbor))
                {
                    pointsInBasin.Add(rightNeighbor);
                    newPoints.Add(rightNeighbor);
                }
                
                var topNeighbor = new Tuple<int,int>(y - 1, x);
                if (y > 0 && points[y - 1, x] < 9 && !pointsInBasin.Contains(topNeighbor))
                {
                    pointsInBasin.Add(topNeighbor);
                    newPoints.Add(topNeighbor);
                }
                
                var bottomNeighbor = new Tuple<int,int>(y + 1, x);
                if (y < height - 1 && points[y + 1, x] < 9 && !pointsInBasin.Contains(bottomNeighbor))
                {
                    pointsInBasin.Add(bottomNeighbor);
                    newPoints.Add(bottomNeighbor);
                }
            }
            pointsToVisit = newPoints;
        }
        
        return pointsInBasin.Count;
    }
}
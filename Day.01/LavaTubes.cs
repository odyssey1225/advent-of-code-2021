namespace Day01;

public static class LavaTubes
{
    public static int GetLowPointRiskLevels(int[,] points)
    {
        var height = points.GetLength(0);
        var width = points.GetLength(1);
        var riskSum = 0;
        
        for(var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (points.IsLowPoint(x, y, height, width))
                {
                    riskSum += points[y, x] + 1;
                }
            }
        }
        
        return riskSum;
    }

    private static bool IsLowPoint(this int[,] points, int x, int y, int height, int width)
    {
        return (x == 0 || points[y, x] < points[y, x - 1])
               && (x == width - 1 || points[y, x] < points[y, x + 1])
               && (y == 0 || points[y, x] < points[y - 1, x])
               && (y == height - 1 || points[y, x] < points[y + 1, x]);
    }
}
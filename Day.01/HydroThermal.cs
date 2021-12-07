using System.Drawing;

namespace Day01
{
    internal class HydroThermal
    {
        public static int GetDangerousPoints(IEnumerable<Tuple<Point, Point>> points)
        {
            var grid = BuildGrid(points);

            foreach (var line in points)
            {
                grid.PlotLine(line);
            }

            return grid.DangerousPoints();
        }

        private static int[,] BuildGrid(IEnumerable<Tuple<Point,Point>> points)
        {
            var size = GetGridSize(points);
            return new int[size, size];
        }

        private static int GetGridSize(IEnumerable<Tuple<Point,Point>> points)
        {
            return points
                .Select(s => s.Item1.X)
                .Union(points.Select(s => s.Item2.X))
                .Union(points.Select(s => s.Item1.Y))
                .Union(points.Select(s => s.Item2.Y))
                .Max() + 1;
        }
    }

    internal static class HyrdroThermalExtensions
    {
        internal static int DangerousPoints(this int[,] grid, int dangerThreshold = 1)
        {
            var dangerousPoints = 0;

            foreach (var point in grid)
            {
                if (point > dangerThreshold)
                {
                    dangerousPoints++;
                }
            }

            return dangerousPoints;
        }

        internal static void PlotLine(this int[,] grid, Tuple<Point, Point> line)
        {
            var xDiff = GetPointDifference(line.Item1.X, line.Item2.X);
            var yDiff = GetPointDifference(line.Item1.Y, line.Item2.Y);
            var numPoints = xDiff == PointDifference.Equal
                ? NumberOfPoints(line.Item1.Y, line.Item2.Y)
                : NumberOfPoints(line.Item1.X, line.Item2.X);

            for (int i = 0; i <= numPoints; i++)
            {
                grid[NextPoint(line.Item1.X, i, xDiff), NextPoint(line.Item1.Y, i, yDiff)]++;
            }
        }

        private static int NextPoint(int current, int index, PointDifference difference)
        {
            return difference switch
            {
                PointDifference.Equal => current,
                PointDifference.LessThan => current + index,
                PointDifference.GreaterThan => current - index,
                _ => throw new ArgumentOutOfRangeException(nameof(difference))
            };
        }

        private static PointDifference GetPointDifference(int point1, int point2)
        {
            return true switch
            {
                bool _ when point1 == point2 => PointDifference.Equal,
                bool _ when point1 > point2 => PointDifference.GreaterThan,
                bool _ when point1 < point2 => PointDifference.LessThan,
                _ => throw new Exception()
            };
        }

        private static int NumberOfPoints(int start, int end)
        {
            return Math.Abs(start - end);
        }
    }

    public enum PointDifference
    {
        Equal = 0,
        GreaterThan,
        LessThan
    }
}

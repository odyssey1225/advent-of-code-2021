using System.Drawing;

namespace Day01
{
    internal class HydroThermal
    {
        public static int GetDangerousPoints(IEnumerable<Tuple<Point, Point>> points)
        {
            var filteredPoints = points
                .Where(w => HyrdroThermalExtensions.IsHorizontalLine(w) || HyrdroThermalExtensions.IsVerticalLine(w))
                .ToList();

            var grid = BuildGrid(filteredPoints);

            foreach (var line in filteredPoints)
            {
                grid.Plot(line);
            }

            return grid.DangerousPointCount();
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
        internal static int DangerousPointCount(this int[,] grid, int dangerThreshold = 1)
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

        internal static int GetLineLength(int start, int end)
        {
            return start > end ? start - end : end - start;
        }

        internal static int GetStartingPoint(int start, int end)
        {
            return start < end ? start : end;
        }

        internal static bool IsHorizontalLine(Tuple<Point, Point> line)
        {
            return line.Item1.Y == line.Item2.Y;
        }

        internal static bool IsVerticalLine(Tuple<Point, Point> line)
        {
            return line.Item1.X == line.Item2.X;
        }

        internal static void Plot(this int[,] grid, Tuple<Point, Point> line)
        {
            var isHorizontalLine = IsHorizontalLine(line);

            var constant = isHorizontalLine
                ? line.Item1.Y
                : line.Item1.X;

            var lineLength = isHorizontalLine
                ? GetLineLength(line.Item1.X, line.Item2.X)
                : GetLineLength(line.Item1.Y, line.Item2.Y);

            var startingPoint = isHorizontalLine
                ? GetStartingPoint(line.Item1.X, line.Item2.X)
                : GetStartingPoint(line.Item1.Y, line.Item2.Y);

            for (int i = 0; i <= lineLength; i++)
            {
                if (isHorizontalLine)
                {
                    grid[startingPoint + i, constant]++;
                }
                else
                {
                    grid[constant, startingPoint + i]++;
                }
            }
        }
    }
}

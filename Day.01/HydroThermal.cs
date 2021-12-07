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
                switch (HyrdroThermalExtensions.LineType(line))
                {
                    case LineType.Horizontal:
                        grid.PlotHorizontalLine(line);
                        break;
                    case LineType.Vertical:
                        grid.PlotVerticalLine(line);
                        break;
                    case LineType.Diagonal:
                        grid.PlotDiagonalLine(line);
                        break;
                }
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

        internal static LineType LineType(Tuple<Point,Point> line)
        {
            return IsHorizontalLine(line)
                ? Day01.LineType.Horizontal
                : IsVerticalLine(line)
                    ? Day01.LineType.Vertical 
                    : Day01.LineType.Diagonal;
        }

        internal static void PlotHorizontalLine(this int[,] grid, Tuple<Point, Point> line)
        {
            var startingPoint = GetStartingPoint(line.Item1.X, line.Item2.X);

            for (int i = 0; i <= NumberOfPoints(line.Item1.X, line.Item2.X); i++)
            {
                grid[startingPoint + i, line.Item1.Y]++;
            }
        }

        internal static void PlotVerticalLine(this int[,] grid, Tuple<Point, Point> line)
        {
            var startingPoint = GetStartingPoint(line.Item1.Y, line.Item2.Y);

            for (int i = 0; i <= NumberOfPoints(line.Item1.Y, line.Item2.Y); i++)
            {
                grid[line.Item1.X, startingPoint + i]++;
            }
        }

        internal static void PlotDiagonalLine(this int[,] grid, Tuple<Point, Point> line)
        {
            var horizontalIsIncreasing = line.Item1.X < line.Item2.X;
            var verticalIsIncreasing = line.Item1.Y < line.Item2.Y;

            for (int i = 0; i <= NumberOfPoints(line.Item1.X, line.Item2.X); i++)
            {
                var x = horizontalIsIncreasing
                    ? line.Item1.X + i
                    : line.Item1.X - i;

                var y = verticalIsIncreasing
                    ? line.Item1.Y + i
                    : line.Item1.Y - i;

                grid[x, y]++;
            }
        }

        private static int NumberOfPoints(int start, int end)
        {
            return start > end ? start - end : end - start;
        }

        private static int GetStartingPoint(int start, int end)
        {
            return start < end ? start : end;
        }

        private static bool IsHorizontalLine(Tuple<Point, Point> line)
        {
            return line.Item1.Y == line.Item2.Y;
        }

        private static bool IsVerticalLine(Tuple<Point, Point> line)
        {
            return line.Item1.X == line.Item2.X;
        }
    }

    public enum LineType
    {
        Horizontal = 0,
        Vertical,
        Diagonal
    }
}

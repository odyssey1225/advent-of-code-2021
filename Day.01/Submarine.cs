using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    internal class Submarine
    {
        public void Navigate(string[] commands)
        {
            var navigation = new Navigation();
            for(int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine(navigation.ChangePosition(commands[i]));
            }
            Console.WriteLine();            
            Console.WriteLine("-------------------------------");            
            Console.WriteLine();            
            Console.WriteLine($"Finished navigating, horizontal position = {navigation.HorizontalPosition} " +
                $"depth = {navigation.Depth}, product = {navigation.HorizontalPosition * navigation.Depth}.");
        }

        public void MeasureDepth(int[] depthMeasurements)
        {
            var depthReader = new DepthReader(depthMeasurements);            
            Console.WriteLine($"Total number of depth measurements: {depthReader.TotalDepthSamples}.");
            Console.WriteLine($"Total number of depth increases: {depthReader.DepthIncreases}.");
            Console.WriteLine($"Total number of depth group increases: {depthReader.GroupedDepthIncreases}.");
        }
    }

    internal class Navigation
    {
        private int _depth;
        public int Depth { get => _depth; }

        private int _horizontalPosition;
        public int HorizontalPosition { get => _horizontalPosition; }

        public string ChangePosition(string command)
        {
            var (direction, change) = ParseNavigation(command);

            switch (direction)
            {
                case Direction.forward:
                    _horizontalPosition += change;
                    break;
                case Direction.back:
                    _horizontalPosition -= change;
                    break;
                case Direction.up:
                    _depth -= change;
                    break;
                case Direction.down:
                    _depth += change;
                    break;
                default:
                    return "Invalid command.";
            }

            return $"Moved {direction} {change} units.";
        }

        private Tuple<Direction, int> ParseNavigation(string command)
        {
            var inputs = command.Split(" ");
            return new Tuple<Direction, int>(Enum.Parse<Direction>(inputs[0]), int.Parse(inputs[1]));
        }
    }

    internal enum Direction
    {
        forward = 0,
        back,
        up,
        down
    }
}

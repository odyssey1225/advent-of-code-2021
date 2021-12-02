namespace Day01
{
    internal class Navigation
    {
        private int _aim;
        public int Aim { get => _aim; }

        private int _depth;
        public int Depth { get => _depth; }

        private int _horizontalPosition;
        public int HorizontalPosition { get => _horizontalPosition; }

        public Navigation(IEnumerable<string> commands)
        {
            for (int i = 0; i < commands.Count(); i++)
            {
                ChangePosition(commands.ElementAt(i));
            }
        }

        private string ChangePosition(string command)
        {
            var (direction, change) = ParseNavigation(command);

            switch (direction)
            {
                case Direction.forward:
                    _horizontalPosition += change;
                    _depth += _aim * change;
                    break;
                case Direction.up:
                    _aim -= change;
                    break;
                case Direction.down:
                    _aim += change;
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
        up,
        down
    }
}

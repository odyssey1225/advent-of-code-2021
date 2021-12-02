namespace Day01
{
    internal class Submarine
    {
        public void Navigate(string[] commands)
        {
            var navigation = new Navigation(commands);
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
}

namespace Day01
{
    public class DepthReader
    {
        private readonly ICollection<int> _depthSamples;

        public readonly int TotalDepthSamples;
        public readonly int DepthIncreases;
        public readonly int GroupedDepthIncreases;

        public DepthReader(ICollection<int> depthSamples, int groupSize = 3)
        {
            _depthSamples = depthSamples;
            TotalDepthSamples = depthSamples.Count;
            DepthIncreases = GetDepthIncreases(depthSamples);
            GroupedDepthIncreases = GetDepthIncreases(GetMeasurementGroups(groupSize));
        }

        private int GetDepthIncreases(ICollection<int> depthMeasurements)
        {
            var increases = 0;

            for (var i = 1; i < depthMeasurements.Count; i++)
            {
                if (depthMeasurements.ElementAt(i) > depthMeasurements.ElementAt(i - 1))
                {
                    increases++;
                }
            }

            return increases;
        }

        private ICollection<int> GetMeasurementGroups(int groupSize)
        {
            var groups = new List<int>();

            for (var i = 0; i < _depthSamples.Count - (groupSize - 1); i++)
            {
                groups.Add(_depthSamples.Take(new Range(i, i + groupSize)).Sum());
            }

            return groups;
        }
    }
}


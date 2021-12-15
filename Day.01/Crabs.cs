namespace Day01
{
    internal class Crabs
    {
        public static int FindCheapestPosition(List<int> crabPositions)
        {
            var mostCommon = crabPositions.GroupBy(g => g).OrderBy(o => o.Count()).Select(s => s.Key);
            var cost = 0;

            foreach(var position in mostCommon)
            {
                var currentCost = crabPositions.Select(s => Math.Abs(s - position)).Sum();
                if (currentCost < cost || cost == 0) cost = currentCost;
            }

            return cost;
        }

        public static int FindCheapestPositionWithCrabMath(List<int> crabPositions)
        {
            var avg = crabPositions.Average();

            var upperBound = (int)Math.Ceiling(avg);
            var lowerBound = (int)Math.Floor(avg);

            var upperBoundCost = crabPositions.Select(s => CrabCost(Math.Abs(s - upperBound))).Sum();
            var lowerBoundCost = crabPositions.Select(s => CrabCost(Math.Abs(s - lowerBound))).Sum();

            return lowerBoundCost < upperBoundCost ? lowerBoundCost : upperBoundCost;
        }

        private static int CrabCost(int distance)
        {
            return distance + (distance > 1 ? CrabCost(--distance) : 0);
        }
    }
}

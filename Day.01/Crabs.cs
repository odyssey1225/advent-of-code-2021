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
    }
}

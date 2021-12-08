namespace Day01
{
    internal class FishSimulator
    {
        public static double SimulateLanternFish(List<int> fish, int days)
        {
            var fishTracker = new List<double>(Enumerable.Repeat(0d, 9));

            foreach (var distinct in fish.Distinct())
            {
                fishTracker[distinct] = fish.Where(w => w == distinct).Count();
            }

            for(int i = 0; i < days; i++)
            {
                var spawnedFish = fishTracker[0];
                fishTracker[0] = fishTracker[1];
                fishTracker[1] = fishTracker[2];
                fishTracker[2] = fishTracker[3];
                fishTracker[3] = fishTracker[4];
                fishTracker[4] = fishTracker[5];
                fishTracker[5] = fishTracker[6];
                fishTracker[6] = fishTracker[7] + spawnedFish;
                fishTracker[7] = fishTracker[8];
                fishTracker[8] = spawnedFish;
            }

            return fishTracker.Sum();
        }
    }
}

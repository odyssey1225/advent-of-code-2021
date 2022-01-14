namespace Day01;

public class Day12
{
    public static void MapCaveNodes(IEnumerable<string> data)
    {
        var caveDictionary = new Dictionary<string, HashSet<string>>();
        
        foreach (var str in data)
        {
            var nodes = str.Split("-");
            
            if (!caveDictionary.ContainsKey(nodes[0]))
            {
                caveDictionary.Add(nodes[0], new HashSet<string>());
            }

            if (!caveDictionary.ContainsKey(nodes[1]))
            {
                caveDictionary.Add(nodes[1], new HashSet<string>());
            }

            caveDictionary[nodes[0]].Add(nodes[1]);
            caveDictionary[nodes[1]].Add(nodes[0]);
        }

        var paths = new List<CavePath> { new(new List<string>(), "start") };

        while (!paths.TrueForAll(p => p.IsComplete))
        {
            var newPaths = paths.Where(w => w.IsComplete).ToList();
            
            foreach (var p in paths.Where(p => !p.IsComplete))
            {
                newPaths.AddRange(caveDictionary[p.LastVisited]
                    .Where(w => p.CanVisit(w))
                    .Select(s => new CavePath(p.VisitedCaves, s))
                    .ToList());
            }

            paths = newPaths;
        }
    }
}

public class CavePath
{
    public CavePath(IEnumerable<string> visitedCaves, string nextCave)
    {
        VisitedCaves = new List<string>(visitedCaves);
        
        if (CanVisit(nextCave))
        {
            VisitedCaves.Add(nextCave);
        }
    }
    
    public readonly List<string> VisitedCaves;
    
    public string LastVisited => VisitedCaves.Last();
    
    public bool IsComplete => LastVisited.Equals("end");

    public bool CanVisit(string cave) => !IsSmallCave(cave) || !VisitedCaves.Contains(cave);

    private static bool IsSmallCave(string cave) => cave.ToLowerInvariant() == cave;

    public override string ToString() => string.Join(",", VisitedCaves);
}

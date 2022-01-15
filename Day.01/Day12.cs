namespace Day01;

public class Day12
{
    public static int TotalPathsSingleSmallCaveVisit(IEnumerable<string> data) =>
        GetCavePaths(MapCaveConnections(data), false).Count;
     

    public static int TotalPathsOneMultipleSmallCaveVisit(IEnumerable<string> data) =>
        GetCavePaths(MapCaveConnections(data), true).Count;

    private static List<CavePath> GetCavePaths(Dictionary<string, HashSet<string>> caveConnections, bool allowSingleMultiSmallCaveVisit)
    {
        var paths = new List<CavePath> { new(new List<string>(), "start") };

        while (!paths.TrueForAll(p => p.IsComplete))
        {
            var newPaths = paths.Where(w => w.IsComplete).ToList();
            
            foreach (var p in paths.Where(p => !p.IsComplete))
            {
                newPaths.AddRange(caveConnections[p.LastVisited]
                    .Where(w => p.CanVisit(w))
                    .Select(s => new CavePath(p.VisitedCaves, s, allowSingleMultiSmallCaveVisit))
                    .ToList());
            }

            paths = newPaths;
        }

        return paths;
    }

    private static Dictionary<string, HashSet<string>> MapCaveConnections(IEnumerable<string> data)
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

        return caveDictionary;
    }
}

public class CavePath
{
    public CavePath(IEnumerable<string> visitedCaves, string nextCave, bool allowSingleMultipleSmallCave = false)
    {
        VisitedCaves = new List<string>(visitedCaves);
        _allowSingleMultipleSmallCaveVisit = allowSingleMultipleSmallCave;
        
        if (CanVisit(nextCave))
        {
            VisitedCaves.Add(nextCave);
        }
    }
    
    public readonly List<string> VisitedCaves;
    private readonly bool _allowSingleMultipleSmallCaveVisit;
    
    public string LastVisited => VisitedCaves.Last();
    
    public bool IsComplete => LastVisited.Equals("end");

    public bool CanVisit(string cave) => !IsSmallCave(cave) || !HasVisited(cave);

    private bool HasVisited(string cave) =>
        _allowSingleMultipleSmallCaveVisit
            ? VisitedCaves.Contains(cave) && HasVisitedSmallCaveTwice(cave)
            : VisitedCaves.Contains(cave);

    private static bool IsSmallCave(string cave) => cave.ToLowerInvariant() == cave;

    private bool HasVisitedSmallCaveTwice(string cave) =>
        cave is "start" or "end" || VisitedCaves
            .Where(IsSmallCave)
            .GroupBy(g => g)
            .Any(a => a.Count() > 1);

    public override string ToString() => string.Join(",", VisitedCaves);
}

// See https://aka.ms/new-console-template for more information

using Utilities;

var inputReader = new InputReader("sampleInput.txt").AllLines;
//var inputReader = new InputReader("input.txt").AllLines;

var cavernGrid = new Grid<CavernVertex>(inputReader.Length, inputReader.First().Length);

for (var y = 0; y < cavernGrid.Height; y++)
{
    for (var x = 0; x < cavernGrid.Width; x++)
    {
        cavernGrid.SetItemAt(x, y, new CavernVertex(x, y, int.Parse(inputReader[y][x].ToString())));
    }
}

var target = cavernGrid.ItemAt(cavernGrid.Width - 1, cavernGrid.Height - 1);

var explored = new List<CavernVertex>();

var frontier = new PriorityQueue<CavernVertex, int>();
frontier.Enqueue(cavernGrid.ItemAt(0, 0), 0);

while (true)
{
    if (!frontier.TryDequeue(out var current, out var priority))
    {
        throw new Exception("No remaining nodes.");
    }
    
    if (current == target)
    {
        Console.WriteLine($"Priority: {priority}");
        break;
    }
    
    explored.Add(current);
    
    foreach (var neighbor in GetNeighbors(current))
    {
        var neighborDistance = priority + neighbor.Value;
        
        var (nifn, nifp) = frontier.UnorderedItems
            .FirstOrDefault(a => a.Element.X == neighbor.X && a.Element.Y == neighbor.Y);
        
        if ((!explored.Contains(neighbor) && nifn is null) || nifp > neighborDistance)
        {
            frontier.Enqueue(neighbor, neighborDistance);
        }
    }
}



List<CavernVertex> GetNeighbors(CavernVertex current)
{
    if (cavernGrid is null)
    {
        throw new Exception("Cavern grid is null.");
    }
    
    var neighbors = new List<CavernVertex>();

    var (x, y, _) = current;
    
    if (cavernGrid.HasBottomNeighbor(y))
    {
        neighbors.Add(cavernGrid.BottomNeighbor(x, y));
    }

    if (cavernGrid.HasRightNeighbor(x))
    {
        neighbors.Add(cavernGrid.RightNeighbor(x, y));
    }

    if (cavernGrid.HasLeftNeighbor(x))
    {
        neighbors.Add(cavernGrid.LeftNeighbor(x, y));
    }

    if (cavernGrid.HasTopNeighbor(y))
    {
        neighbors.Add(cavernGrid.TopNeighbor(x, y));
    }
    
    return neighbors;
}

public record CavernVertex(int X, int Y, int Value)
{
    public readonly int X = X;
    public readonly int Y = Y;
    public readonly int Value = Value;
}

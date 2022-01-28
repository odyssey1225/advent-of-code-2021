using Utilities;

namespace Day15;

public static class CavernHelpers
{
    private static List<CavernNode> GetNeighbors(this Grid<CavernNode> grid, CavernNode current)
    {
        if (grid is null)
        {
            throw new Exception("Cavern grid is null.");
        }
    
        var neighbors = new List<CavernNode>();

        var (x, y, _) = current;
    
        if (grid.HasBottomNeighbor(y))
        {
            neighbors.Add(grid.BottomNeighbor(x, y));
        }

        if (grid.HasRightNeighbor(x))
        {
            neighbors.Add(grid.RightNeighbor(x, y));
        }

        if (grid.HasLeftNeighbor(x))
        {
            neighbors.Add(grid.LeftNeighbor(x, y));
        }

        if (grid.HasTopNeighbor(y))
        {
            neighbors.Add(grid.TopNeighbor(x, y));
        }
    
        return neighbors;
    }
    
    public static int GetShortestPath(this Grid<CavernNode> grid)
    {
        var target = grid.ItemAt(grid.Width - 1, grid.Height - 1);
        var explored = new List<CavernNode>();
        var frontier = new PriorityQueue<CavernNode, int>();
    
        frontier.Enqueue(grid.ItemAt(0, 0), 0);

        var nodeTicks = 0;
        var neighborTicks = 0;
        var timestamp = DateTime.Now;

        while (true)
        {
            nodeTicks++;
            if (!frontier.TryDequeue(out var node, out var priority))
            {
                throw new Exception("No remaining nodes.");
            }
    
            if (node == target)
            {
                Console.WriteLine($"Visited {nodeTicks} nodes");
                Console.WriteLine($"Visited {neighborTicks} neighbor nodes");
                Console.WriteLine($"Total nodes in graph {grid.AllItems().Count}");
                Console.WriteLine($"Finished in {DateTime.Now.Subtract(timestamp).TotalSeconds} seconds");
                return priority;
            }
    
            explored.Add(node);
    
            foreach (var neighbor in grid.GetNeighbors(node))
            {
                neighborTicks++;
                var neighborDistance = priority + neighbor.Value;
        
                var (nifn, nifp) = frontier.UnorderedItems.FirstOrDefault(a => a.Element == neighbor);

                if ((!explored.Contains(neighbor) && nifn is null) || nifp > neighborDistance)
                {
                    frontier.Enqueue(neighbor, neighborDistance);
                }
            }
        }
    }
    
    public static Grid<CavernNode> ExpandGrid(this Grid<CavernNode> original, int size)
    {
        int IncrementValue(int value) => value == 9 ? 1 : value + 1;
        
        var expandedHeight = original.Height * size;
        var expandedWidth = original.Width * size;
        
        var expandedGrid = new Grid<CavernNode>(expandedHeight, expandedWidth);
        
        for (var y = 0; y < original.Height; y++)
        {
            for (var x = 0; x < original.Width; x++)
            {
                expandedGrid.SetItemAt(x, y, original.ItemAt(x, y));
            }
        }
        
        var horizontalIncrement = 0;
        
        while (horizontalIncrement < size - 1)
        {
            var oldX = original.Width * horizontalIncrement;
            var newWidth = original.Width * ++horizontalIncrement;
            
            for (var y = 0; y < original.Height; y++)
            {
                for (var x = 0; x < original.Width; x++)
                {
                    var newX = newWidth + x;
                    var newValue = IncrementValue(expandedGrid.ItemAt(x + oldX, y).Value);
                    expandedGrid.SetItemAt(newX, y, new CavernNode(newX, y, newValue));
                }
            }
        }
    
        var verticalIncrement = 0;
        
        while (verticalIncrement < size - 1)
        {
            var oldY = original.Height * verticalIncrement;
            var newHeight = original.Height * ++verticalIncrement;
        
            for (var y = 0; y < original.Height; y++)
            {
                for (var x = 0; x < expandedWidth; x++)
                {
                    var newY = newHeight + y;
                    var newValue = IncrementValue(expandedGrid.ItemAt(x, y + oldY).Value);
                    expandedGrid.SetItemAt(x, newY, new CavernNode(x, newY, newValue));
                }
            }
        }
    
        return expandedGrid;
    }
}
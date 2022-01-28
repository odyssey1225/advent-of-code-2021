using Day15;
using Utilities;

// var inputReader = new InputReader("sampleInput.txt").AllLines;
var inputReader = new InputReader("input.txt").AllLines;

var cavernGrid = new Grid<CavernNode>(inputReader.Length, inputReader.First().Length);

for (var y = 0; y < cavernGrid.Height; y++)
{
    for (var x = 0; x < cavernGrid.Width; x++)
    {
        cavernGrid.SetItemAt(x, y, new CavernNode(x, y, int.Parse(inputReader[y][x].ToString())));
    }
}

Console.WriteLine($"Small Grid Shortest Path: {cavernGrid.GetShortestPath()}");
Console.WriteLine($"Expanded Grid Shortest Path: {cavernGrid.ExpandGrid(5).GetShortestPath()}");

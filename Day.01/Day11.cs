namespace Day01;

public static class Day11
{
    public static int TrackFlashes(int[,] data, int numSteps)
    {
        var octopusGrid = BuildOctopusGrid(data);
        
        while (numSteps > 0)
        {
            --numSteps;
            octopusGrid.ApplyOctopusStep();
        }

        return octopusGrid.AllItems().Sum(s => s.Flashes);
    }

    public static int GetSynchronizedStep(int[,] data)
    {
        var numSteps = 0;
        var octopusGrid = BuildOctopusGrid(data);
        
        while (!octopusGrid.TrueForAll(f => f.EnergyLevel == 0))
        {
            ++numSteps;
            octopusGrid.ApplyOctopusStep();
        }
        
        return numSteps;
    }

    private static Grid<DumboOctopus> BuildOctopusGrid(int[,] data)
    {
        var octopusGrid = new Grid<DumboOctopus>(data.GetLength(0), data.GetLength(1));

        for(var y = 0; y < data.GetLength(0); y++)
        {
            for (var x = 0; x < data.GetLength(1); x++)
            {
                octopusGrid.SetItemAt(x, y, new DumboOctopus(data[y, x]));
            }
        }

        return octopusGrid;
    }
    
    private static void ApplyOctopusStep(this Grid<DumboOctopus> octopusGrid)
    {
        octopusGrid.ForEach(f => f.GainEnergy());

        while (!octopusGrid.TrueForAll(f => f.EnergyLevel <= 9))
        {
            for(var y = 0; y < octopusGrid.Height; y++)
            {
                for (var x = 0; x < octopusGrid.Width; x++)
                {
                    if (octopusGrid.ItemAt(x, y).EnergyLevel <= 9)
                    {
                        continue;
                    }

                    octopusGrid.ItemAt(x, y).Flash();
                    octopusGrid.GetNeighbors(x, y).ForEach(f => f.GainFlashEnergy());
                }
            }
        }
    }
}

public class DumboOctopus
{
    public DumboOctopus(int startingEnergy)
    {
        EnergyLevel = startingEnergy;
    }

    public int EnergyLevel { get; private set; }

    public void GainEnergy() => ++EnergyLevel;

    public void GainFlashEnergy()
    {
        if (EnergyLevel is 0)
        {
            return;
        }
        
        GainEnergy();
    }

    public int Flashes { get; private set; }
    
    public void Flash()
    {
        ++Flashes;
        EnergyLevel = 0;
    }
}

public class Grid<T>
{
    public readonly int Height;
    public readonly int Width;
    private readonly T[,] _data;

    public Grid(int height, int width)
    {
        _data = new T[height, width];
        Height = height;
        Width = width;
    }

    public void ForEach(Action<T> action)
    {
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                action(_data[y, x]);
            }
        }
    }

    public bool TrueForAll(Predicate<T> match)
    {
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (!match(_data[y, x]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public T ItemAt(int x, int y) => _data[y, x];

    public void SetItemAt(int x, int y, T item)
    {
        if (x < 0 || x > Width || y < 0 || y > Height)
        {
            throw new Exception("Index out of bounds.");
        }
        
        _data[y, x] = item;
    }

    public List<T> AllItems()
    {
        var items = new List<T>();
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                items.Add(ItemAt(x, y));
            }
        }
        return items;
    }

    public List<T> GetNeighbors(int x, int y)
    {
        var neighbors = new List<T>();
        
        if (HasLeftNeighbor(x))
        {
            neighbors.Add(LeftNeighbor(x, y));
        }

        if (HasTopLeftNeighbor(x, y))
        {
            neighbors.Add(TopLeftNeighbor(x, y));
        }

        if (HasTopNeighbor(y))
        {
            neighbors.Add(TopNeighbor(x, y));
        }

        if (HasTopRightNeighbor(x, y))
        {
            neighbors.Add(TopRightNeighbor(x, y));
        }

        if (HasRightNeighbor(x))
        {
            neighbors.Add(RightNeighbor(x, y));
        }

        if (HasBottomRightNeighbor(x, y))
        {
            neighbors.Add(BottomRightNeighbor(x, y));
        }

        if (HasBottomNeighbor(y))
        {
            neighbors.Add(BottomNeighbor(x, y));
        }

        if (HasBottomLeftNeighbor(x, y))
        {
            neighbors.Add(BottomLeftNeighbor(x, y));
        }
        
        return neighbors;
    }

    private T LeftNeighbor(int x, int y) => _data[y, x - 1];
    private T TopLeftNeighbor(int x, int y) => _data[y - 1, x - 1];
    private T TopNeighbor(int x, int y) => _data[y - 1, x];
    private T TopRightNeighbor(int x, int y) => _data[y - 1, x + 1];
    private T RightNeighbor(int x, int y) => _data[y, x + 1];
    private T BottomRightNeighbor(int x, int y) => _data[y + 1, x + 1];
    private T BottomNeighbor(int x, int y) => _data[y + 1, x];
    private T BottomLeftNeighbor(int x, int y) => _data[y + 1, x - 1];

    private bool HasLeftNeighbor(int x) => x > 0;
    private bool HasRightNeighbor(int x) => x < Width - 1;
    private bool HasTopNeighbor(int y) => y > 0;
    private bool HasBottomNeighbor(int y) => y < Height - 1;
    private bool HasTopLeftNeighbor(int x, int y) => HasLeftNeighbor(x) && HasTopNeighbor(y);
    private bool HasTopRightNeighbor(int x, int y) => HasRightNeighbor(x) && HasTopNeighbor(y);
    private bool HasBottomRightNeighbor(int x, int y) => HasRightNeighbor(x) && HasBottomNeighbor(y);
    private bool HasBottomLeftNeighbor(int x, int y) => HasLeftNeighbor(x) && HasBottomNeighbor(y);
}

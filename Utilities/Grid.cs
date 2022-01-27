namespace Utilities;

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

    public Grid(int height, int width, T defaultValue) : this(height, width)
    {
        SetDefault(defaultValue);
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

    private void SetDefault(T defaultValue)
    {
        for(var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _data[y, x] = defaultValue;
            }
        }
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

    public T LeftNeighbor(int x, int y) => _data[y, x - 1];
    public T TopLeftNeighbor(int x, int y) => _data[y - 1, x - 1];
    public T TopNeighbor(int x, int y) => _data[y - 1, x];
    public T TopRightNeighbor(int x, int y) => _data[y - 1, x + 1];
    public T RightNeighbor(int x, int y) => _data[y, x + 1];
    public T BottomRightNeighbor(int x, int y) => _data[y + 1, x + 1];
    public T BottomNeighbor(int x, int y) => _data[y + 1, x];
    public T BottomLeftNeighbor(int x, int y) => _data[y + 1, x - 1];
    
    public bool HasLeftNeighbor(int x) => x > 0;
    public bool HasRightNeighbor(int x) => x < Width - 1;
    public bool HasTopNeighbor(int y) => y > 0;
    public bool HasBottomNeighbor(int y) => y < Height - 1;
    public bool HasTopLeftNeighbor(int x, int y) => HasLeftNeighbor(x) && HasTopNeighbor(y);
    public bool HasTopRightNeighbor(int x, int y) => HasRightNeighbor(x) && HasTopNeighbor(y);
    public bool HasBottomRightNeighbor(int x, int y) => HasRightNeighbor(x) && HasBottomNeighbor(y);
    public bool HasBottomLeftNeighbor(int x, int y) => HasLeftNeighbor(x) && HasBottomNeighbor(y);
}
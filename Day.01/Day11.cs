using Utilities;

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

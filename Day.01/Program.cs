// See https://aka.ms/new-console-template for more information
using Day01;

var submarine = new Submarine();

Console.WriteLine("\n --- Sample Navigation --- \n");
submarine.Navigate(SampleData.NavigationCommands);

Console.WriteLine("\n --- My Navigation --- \n");
submarine.Navigate(SubmarineData.NavigationCommands);

Console.WriteLine("\n --- Sample Depth Measurements --- \n");
submarine.MeasureDepth(SampleData.Depths);

Console.WriteLine("\n --- My Depth Measurements --- \n");
submarine.MeasureDepth(SubmarineData.DepthMeasurements);

Console.WriteLine("\n --- Sample Diagnostics --- \n");
var sampleDiagnostics = new Diagnostics(SampleData.DiagnosticsData);
Console.WriteLine($"  Power Consumption: {sampleDiagnostics.GetPowerConsumption()}");
Console.WriteLine($"Life Support Rating: {sampleDiagnostics.GetLifeSupportRating()}");

Console.WriteLine("\n --- My Diagnostics --- \n");
var myDiagnostics = new Diagnostics(SubmarineData.DiagnosticsData); 
Console.WriteLine($"  Power Consumption: {myDiagnostics.GetPowerConsumption()}");
Console.WriteLine($"Life Support Rating: {myDiagnostics.GetLifeSupportRating()}");

var bingoPlayer = new Bingo();

Console.WriteLine("\n --- Sample Bingo Game --- \n");
Console.WriteLine($"First Bingo: {bingoPlayer.GetFirstBingo(SampleData.BingoCards, SampleData.BingoNumbers)}");
Console.WriteLine($"Last Bingo: {bingoPlayer.GetLastBingo(SampleData.BingoCards, SampleData.BingoNumbers)}");

Console.WriteLine("\n --- My Bingo Game --- \n");
Console.WriteLine($"First Bingo: {bingoPlayer.GetFirstBingo(SubmarineData.BingoCards, SubmarineData.BingoNumbers)}");
Console.WriteLine($"Last Bingo: {bingoPlayer.GetLastBingo(SubmarineData.BingoCards, SubmarineData.BingoNumbers)}");

Console.WriteLine("\n --- Sample Hydro Thermal --- \n");
Console.WriteLine($"Dangerous Points: {HydroThermal.GetDangerousPoints(SampleData.HydroThermalVents)}");

Console.WriteLine("\n --- My Hydro Thermal --- \n");
Console.WriteLine($"Dangerous Points: {HydroThermal.GetDangerousPoints(SubmarineData.HydroThermalVents)}");

Console.WriteLine("\n --- Sample Lantern Fish --- \n");
Console.WriteLine($"Lantern Fish after 80 days: {FishSimulator.SimulateLanternFish(SampleData.LanternFish, 80)}");
Console.WriteLine($"Lantern Fish after 256 days: {FishSimulator.SimulateLanternFish(SampleData.LanternFish, 256)}");

Console.WriteLine("\n --- My Lantern Fish --- \n");
Console.WriteLine($"Lantern Fish after 80 days: {FishSimulator.SimulateLanternFish(SubmarineData.LanternFish, 80)}");
Console.WriteLine($"Lantern Fish after 256 days: {FishSimulator.SimulateLanternFish(SubmarineData.LanternFish, 256)}");

Console.WriteLine("\n --- Sample Crab Submarines --- \n");
Console.WriteLine($"Cheapest fuel cost: {Crabs.FindCheapestPosition(SampleData.CrabPositions)}");
Console.WriteLine($"Cheapest crab math fuel cost: {Crabs.FindCheapestPositionWithCrabMath(SampleData.CrabPositions)}");

Console.WriteLine("\n --- My Crab Submarines --- \n");
Console.WriteLine($"Cheapest fuel cost: {Crabs.FindCheapestPosition(SubmarineData.CrabPositions)}");
Console.WriteLine($"Cheapest crab math fuel cost: {Crabs.FindCheapestPositionWithCrabMath(SubmarineData.CrabPositions)}");

Console.WriteLine("\n --- Sample Display Data --- \n");
Console.WriteLine($"Digit count: {DisplayReader.CountDisplayDigits(SampleData.DisplayData)}");
Console.WriteLine($"Sum of Displayed Digits: {DisplayReader.ReadDisplay(SampleData.DisplayData)}");

Console.WriteLine("\n --- My Display Data --- \n");
Console.WriteLine($"Digits displayed: {DisplayReader.CountDisplayDigits(SubmarineData.DisplayData)}");
Console.WriteLine($"Sum of Displayed Digits: {DisplayReader.ReadDisplay(SubmarineData.DisplayData)}");

Console.WriteLine("\n --- Sample Lava Tubes Data --- \n");
Console.WriteLine($"Lava Tubes Risky Points Sum: {LavaTubes.GetLowPointRiskLevels(SampleData.LowPoints)}");
Console.WriteLine($"Lava Tube Largest Basins: {LavaTubes.GetLargestBasins(SampleData.LowPoints)}");

Console.WriteLine("\n --- My Lava Tubes Data --- \n");
Console.WriteLine($"Lava Tubes Risky Points Sum: {LavaTubes.GetLowPointRiskLevels(SubmarineData.LowPoints)}");
Console.WriteLine($"Lava Tube Largest Basins: {LavaTubes.GetLargestBasins(SubmarineData.LowPoints)}");

Console.WriteLine("\n --- Sample Day 10 --- \n");
Console.WriteLine($"Illegal Character Score: {Day10.GetCorruptLineScore(SampleData.Day10Input)}");
Console.WriteLine($"Incomplete Line Score: {Day10.GetIncompleteLineScore(SampleData.Day10Input)}");

Console.WriteLine("\n --- My Day 10 --- \n");
Console.WriteLine($"Illegal Character Score: {Day10.GetCorruptLineScore(SubmarineData.Day10Input)}");
Console.WriteLine($"Incomplete Line Score: {Day10.GetIncompleteLineScore(SubmarineData.Day10Input)}");

Console.WriteLine("\n --- Sample Day 11 --- \n");
Console.WriteLine($"Total Flashes: {Day11.TrackFlashes(SampleData.DumboOctopuses, 100)}");
Console.WriteLine($"Synchronized on Step: {Day11.GetSynchronizedStep(SampleData.DumboOctopuses)}");

Console.WriteLine("\n --- My Day 11 --- \n");
Console.WriteLine($"Total Flashes: {Day11.TrackFlashes(SubmarineData.DumboOctopuses, 100)}");
Console.WriteLine($"Synchronized on Step: {Day11.GetSynchronizedStep(SubmarineData.DumboOctopuses)}");

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

Console.WriteLine("\n --- My Diagnositcs --- \n");
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

Console.WriteLine("\n --- Sample Hyrdro Thermal --- \n");
Console.WriteLine($"Dangerous Points: {HydroThermal.GetDangerousPoints(SampleData.HydroThermalVents)}");

Console.WriteLine("\n --- My Hyrdro Thermal --- \n");
Console.WriteLine($"Dangerous Points: {HydroThermal.GetDangerousPoints(SubmarineData.HydroThermalVents)}");

Console.WriteLine("\n --- Sample Lantern Fish --- \n");
Console.WriteLine($"Lantern Fish after 80 days: {FishSimulator.SimulateLanternFish(SampleData.LanternFish, 80)}");
Console.WriteLine($"Lantern Fish after 256 days: {FishSimulator.SimulateLanternFish(SampleData.LanternFish, 256)}");

Console.WriteLine("\n --- My Lantern Fish --- \n");
Console.WriteLine($"Lantern Fish after 80 days: {FishSimulator.SimulateLanternFish(SubmarineData.LanternFish, 80)}");
Console.WriteLine($"Lantern Fish after 256 days: {FishSimulator.SimulateLanternFish(SubmarineData.LanternFish, 256)}");

// See https://aka.ms/new-console-template for more information

using Day01;

var sampleDepths = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
var sampleDiagnosticData = new string[] { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };
var sampleNavigationCommands = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

var submarine = new Submarine();

Console.WriteLine("\n --- Sample Navigation --- \n");
submarine.Navigate(sampleNavigationCommands);

Console.WriteLine("\n --- My Navigation --- \n");
submarine.Navigate(SubmarineData.NavigationCommands);

Console.WriteLine("\n --- Sample Depth Measurements --- \n");
submarine.MeasureDepth(sampleDepths);

Console.WriteLine("\n --- My Depth Measurements --- \n");
submarine.MeasureDepth(SubmarineData.DepthMeasurements);

Console.WriteLine("\n --- Sample Power Consumption --- \n");
Console.WriteLine(Diagnostics.GetPowerConsumption(sampleDiagnosticData));

Console.WriteLine("\n --- My Power Consumption --- \n");
Console.WriteLine(Diagnostics.GetPowerConsumption(SubmarineData.DiagnosticsData));

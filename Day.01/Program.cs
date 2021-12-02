// See https://aka.ms/new-console-template for more information

using Day01;

var sampleDepths = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
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

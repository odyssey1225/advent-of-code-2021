// See https://aka.ms/new-console-template for more information

using Day01;

var sampleDepths = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

var sampleDepthReader = new DepthReader(sampleDepths);
var myDepthReader = new DepthReader(SubmarineData.DepthMeasurements);

Console.WriteLine($"Total number of sample depth measurements: {sampleDepthReader.TotalDepthSamples}.");
Console.WriteLine($"Total number of sample depth increases: {sampleDepthReader.DepthIncreases}.");
Console.WriteLine($"Total number of sample depth group increases: {sampleDepthReader.GroupedDepthIncreases}.");

Console.WriteLine($"Total number of my depth measurements: {myDepthReader.TotalDepthSamples}.");
Console.WriteLine($"Total number of my depth increases: {myDepthReader.DepthIncreases}.");
Console.WriteLine($"Total number of my depth group increases: {myDepthReader.GroupedDepthIncreases}.");

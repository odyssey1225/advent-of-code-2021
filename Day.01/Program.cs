// See https://aka.ms/new-console-template for more information

using Day01;

var samples = new List<int> { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

var depthReader = new DepthReader(DepthSamples.AllSamples);

Console.WriteLine($"Total number of samples: {depthReader.TotalDepthSamples}.");
Console.WriteLine($"Total number of depth increases: {depthReader.DepthIncreases}.");

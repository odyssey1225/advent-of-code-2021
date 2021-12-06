﻿// See https://aka.ms/new-console-template for more information

using Day01;

var sampleDepths = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
var sampleDiagnosticData = new string[] { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };
var sampleNavigationCommands = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
var SampleBingoNumbers = new List<int>()
{
    7,
    4,
    9,
    5,
    11,
    17,
    23,
    2,
    0,
    14,
    21,
    24,
    10,
    16,
    13,
    6,
    15,
    25,
    12,
    22,
    18,
    20,
    8,
    19,
    3,
    26,
    1
};
var sampleBingoCards = new List<int>()
{
    22,
    13,
    17,
    11,
    0,
    8,
    2,
    23,
    4,
    24,
    21,
    9,
    14,
    16,
    7,
    6,
    10,
    3,
    18,
    5,
    1,
    12,
    20,
    15,
    19,
    3,
    15,
    0,
    2,
    22,
    9,
    18,
    13,
    17,
    5,
    19,
    8,
    7,
    25,
    23,
    20,
    11,
    10,
    24,
    4,
    14,
    21,
    16,
    12,
    6,
    14,
    21,
    17,
    24,
    4,
    10,
    16,
    15,
    9,
    19,
    18,
    8,
    23,
    26,
    20,
    22,
    11,
    13,
    6,
    5,
    2,
    0,
    12,
    3,
    7
};

var submarine = new Submarine();

Console.WriteLine("\n --- Sample Navigation --- \n");
submarine.Navigate(sampleNavigationCommands);

Console.WriteLine("\n --- My Navigation --- \n");
submarine.Navigate(SubmarineData.NavigationCommands);

Console.WriteLine("\n --- Sample Depth Measurements --- \n");
submarine.MeasureDepth(sampleDepths);

Console.WriteLine("\n --- My Depth Measurements --- \n");
submarine.MeasureDepth(SubmarineData.DepthMeasurements);

Console.WriteLine("\n --- Sample Diagnostics --- \n");
var sampleDiagnostics = new Diagnostics(sampleDiagnosticData);
Console.WriteLine($"  Power Consumption: {sampleDiagnostics.GetPowerConsumption()}");
Console.WriteLine($"Life Support Rating: {sampleDiagnostics.GetLifeSupportRating()}");

Console.WriteLine("\n --- My Diagnositcs --- \n");
var myDiagnostics = new Diagnostics(SubmarineData.DiagnosticsData);
Console.WriteLine($"  Power Consumption: {myDiagnostics.GetPowerConsumption()}");
Console.WriteLine($"Life Support Rating: {myDiagnostics.GetLifeSupportRating()}");

Console.WriteLine("\n --- Sample Bingo Game --- \n");
var sampleBingo = new Bingo(sampleBingoCards, SampleBingoNumbers);
Console.WriteLine($"First Bingo: {sampleBingo.GetEarliestBingo()}");
Console.WriteLine($"Last Bingo: {sampleBingo.GetLastBingo()}");

Console.WriteLine("\n --- My Bingo Game --- \n");
var myBingo = new Bingo(SubmarineData.BingoCards, SubmarineData.BingoNumbers);
Console.WriteLine($"First Bingo: {myBingo.GetEarliestBingo()}");
Console.WriteLine($"Last Bingo: {myBingo.GetLastBingo()}");

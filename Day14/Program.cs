// See https://aka.ms/new-console-template for more information

using Utilities;

// var inputReader = new InputReader("sampleInput.txt");
var inputReader = new InputReader("input.txt");

var patternMap = inputReader.Lines[2..]
    .Select(s => s.Split("->"))
    .ToDictionary(k => k[0].Trim(), v => v[1].Trim());

var polymerPairs = patternMap
    .Select(s => s.Key)
    .ToDictionary(k => k, v => 0L);

var lastGroup = string.Empty;

var initialPolymer = inputReader.Lines[0];

for (var i = 0; i < initialPolymer.Length - 1; i++)
{
    lastGroup = initialPolymer[i..(i + 2)];
    polymerPairs[lastGroup]++;
}

var steps = 0;

while (steps < 40)
{
    steps++;

    lastGroup = patternMap[lastGroup] + lastGroup[1];

    var newGroups = patternMap
        .Select(s => s.Key)
        .ToDictionary(k => k, v => 0L);

    foreach (var (key, value) in polymerPairs.Where(w => w.Value > 0))
    {
        newGroups[key[0] + patternMap[key]] += value;
        newGroups[patternMap[key] + key[1]] += value;
    }

    polymerPairs = newGroups;
}

var characterScore = polymerPairs
    .GroupBy(g => g.Key[0])
    .ToDictionary(k => k.Key, v => v.Sum(s => s.Value));

characterScore[lastGroup[^1]] += 1;

var (minKey, minValue) = characterScore.MinBy(m => m.Value);
var (maxKey, maxValue) = characterScore.MaxBy(m => m.Value);

Console.WriteLine($"Difference between most common ({maxKey} - {maxValue}) and least common " +
                  $"({minKey} - {minValue}): {maxValue - minValue}");



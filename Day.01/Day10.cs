namespace Day01;

public static class Day10
{
    public static long GetCorruptLineScore(IEnumerable<string> input) =>
        input.Select(AnalyzeCodeLine).Where(w => !w.Item1).Sum(s => s.Item2);

    public static long GetIncompleteLineScore(IEnumerable<string> input)
    {
        var lines = input
            .Select(AnalyzeCodeLine)
            .Where(w => w.Item1)
            .OrderBy(o => o.Item2)
            .ToArray();

        return lines.Skip(lines.Length / 2).First().Item2;
    }

    private static Tuple<bool, long> AnalyzeCodeLine(string line)
    {
        var openingChars = new Stack<char>();
        
        foreach (var c in line)
        {
            if (OpeningChars.Contains(c))
            {
                openingChars.Push(c);
                continue;
            }

            if (!IsMatch(openingChars.Peek(), c))
            {
                return new Tuple<bool, long>(false, InvalidCharScore[c]);
            }
            
            openingChars.Pop();
        }

        return new Tuple<bool, long>(true, 
            openingChars.Aggregate(0L, (ag, c) => ag * 5 + AutocompleteCharScore[c]));
    }

    private static bool IsMatch(char a, char b) =>
        a == '(' && b == ')'
        || a == '[' && b == ']'
        || a == '{' && b == '}'
        || a == '<' && b == '>';

    private static readonly char[] OpeningChars = { '(', '[', '{', '<' };

    private static readonly Dictionary<char, int> InvalidCharScore =
        new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

    private static readonly Dictionary<char, int> AutocompleteCharScore =
        new() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
}
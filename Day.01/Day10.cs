namespace Day01;

public static class Day10
{
    public static long GetCorruptionPoints(IEnumerable<string> input)
    {
        var score = 0L;

        foreach (var line in input)
        {
            var openingChars = new Stack<char>();
            
            foreach(var character in line.ToCharArray())
            {
                
                if (IsOpeningChar(character))
                {
                    openingChars.Push(character);
                    continue;
                }

                if (IsMatchingPair(openingChars.Pop(), character))
                {
                    continue;
                }
                
                score += GetScore(character);
                break;
            }
        }
        
        return score;
    }

    private static readonly char[] OpeningChars = { '(', '[', '{', '<' };
    
    private static bool IsOpeningChar(char c)
    {
        return OpeningChars.Contains(c);
    }

    private static bool IsMatchingPair(char a, char b)
    {
        return a == '(' && b == ')'
            || a == '[' && b == ']'
            || a == '{' && b == '}'
            || a == '<' && b == '>';
    }

    private static int GetScore(char c)
    {
        return c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => throw new Exception($"Invalid character \"{c}\"")
        };
    }
}
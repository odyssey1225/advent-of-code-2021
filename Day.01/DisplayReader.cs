namespace Day01
{
    internal static class DisplayReader
    {
        public static int CountDisplayDigits(List<string> displayData)
        {
            List<DisplayDigit> digits = new();

            foreach(var datum in displayData)
            {
                digits.AddRange(datum
                    .Split('|')
                    .Last()
                    .Split(' ')
                    .Select(GetDisplayDigit)
                    .Where(w => w != DisplayDigit.Unknown));
            }

            return digits.Count;
        }

        public static int ReadDisplay(IEnumerable<string> displayData)
        {
            var displayDatalbl = displayData
                .Select(s => new DisplayData(s))
                .Select(s => s.DisplayedDigit());
                
            
            return displayData
                .Select(s => new DisplayData(s))
                .Sum(s => s.DisplayedDigit());
        }

        internal static DisplayDigit GetDisplayDigit(string data)
        {
            return data.Length switch
            {
                2 => DisplayDigit.One,
                3 => DisplayDigit.Seven,
                4 => DisplayDigit.Four,
                7 => DisplayDigit.Eight,
                _ => 0,
            };
        }
    }

    internal class DisplayData
    {
        private readonly string[] _signalPatterns;
        private readonly string[] _displayPatterns;
        private readonly char[] _signalMap;

        public DisplayData(string output)
        {
            _signalPatterns = output
                .Split('|')
                .First()
                .Trim()
                .Split(' ');

            _displayPatterns = output
                .Split('|')
                .Last()
                .Trim()
                .Split(' ');

            _signalMap = MapSignal();
        }

        private char[] MapSignal()
        {
            var map = new char[7];

            var oneSignal = GetSignalForDigit(DisplayDigit.One, map);
            
            map[0] = oneSignal[0];
            map[1] = oneSignal[1];
            
            var fourSignal = GetSignalForDigit(DisplayDigit.Four, map);
            
            map[4] = fourSignal[0];
            map[5] = fourSignal[1];
            
            var sevenSignal = GetSignalForDigit(DisplayDigit.Seven, map);
            
            map[6] = sevenSignal[0];

            var eightSignal = GetSignalForDigit(DisplayDigit.Eight, map);
            
            map[2] = eightSignal[0];
            map[3] = eightSignal[1];
            
            return map;
        }

        private char[] GetSignalForDigit(DisplayDigit digit, IEnumerable<char> signalMap)
        {
            return _signalPatterns
                .First(f => DisplayReader.GetDisplayDigit(f) == digit)
                .Where(w => !signalMap.Contains(w))
                .ToArray();
        }

        public int DisplayedDigit()
        {
            var displayDigits = _displayPatterns.Select(SignalToDigit).ToArray();
            var bloop = displayDigits[0] * 1000
                        + displayDigits[1] * 100
                        + displayDigits[2] * 10
                        + displayDigits[3];
            return displayDigits[0] * 1000 
                   + displayDigits[1] * 100 
                   + displayDigits[2] * 10 
                   + displayDigits[3];
        }

        private int SignalToDigit(string signal)
        {
            return DisplayReader.GetDisplayDigit(signal) switch
            {
                DisplayDigit.One => 1,
                DisplayDigit.Four => 4,
                DisplayDigit.Seven => 7,
                DisplayDigit.Eight => 8,
                _ => ParseDigit(signal)
            };
        }

        // 0, 2, 3, 5, 6, 9
        private int ParseDigit(string digit)
        {
            switch (digit.Length)
            {
                // 2, 3, 5
                case 5:
                    if (!digit.Contains(_signalMap[1]) && !digit.Contains(_signalMap[5])) return 2;
                    if (!digit.Contains(_signalMap[3]) && !digit.Contains(_signalMap[5])) return 3;
                    return 5;

                // 0, 6, 9
                case 6:
                    if (!digit.Contains(_signalMap[4])) return 0;
                    return !digit.Contains(_signalMap[0]) ? 6 : 9;

                default: throw new Exception($"Failed to parse digit {digit}.");
            }
        }
    }
    
    internal enum DisplayDigit
    {
        Unknown,
        One,
        Four,
        Seven,
        Eight
    }
}

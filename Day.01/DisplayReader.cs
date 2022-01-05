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

        /**
         * Display characters mapped by index:
         *      666
         *    5     0
         *    5     0
         *    5     0
         *      444
         *    3     1
         *    3     1
         *    3     1
         *      222
         */
        private char[] MapSignal()
        {
            var map = new char[7];

            var oneSignal = GetSignalForDigit(DisplayDigit.One);
            var sevenSignal = GetSignalForDigit(DisplayDigit.Seven);
            
            map[0] = oneSignal[0];
            map[1] = oneSignal[1];
            map[6] = sevenSignal.First(f => !map.Contains(f));
            
            var fourSignal = GetSignalForDigit(DisplayDigit.Four);
            var eightSignal = GetSignalForDigit(DisplayDigit.Eight);
            var threeSignal = _signalPatterns.First(f => f.Length == 5
                            && f.Contains(map[0])
                            && f.Contains(map[1])
                            && f.Contains(map[6]));

            map[4] = threeSignal.First(f => fourSignal.Contains(f) && !map.Contains(f));
            map[2] = threeSignal.First(f => !map.Contains(f));
            map[5] = fourSignal.First(f => !map.Contains(f));
            map[3] = eightSignal.First(f => !map.Contains(f));

            var fiveSignal = _signalPatterns.First(f => f.Length == 5 
                                                        && f.Contains(map[6])
                                                        && f.Contains(map[5])
                                                        && f.Contains(map[4])
                                                        && f.Contains(map[2]));

            // Verify the one signal characters are in the correct order, if not swap the signals.
            if (!fiveSignal.Contains(map[1]))
            {
                (map[0], map[1]) = (map[1], map[0]);
            }
            
            return map;
        }

        private char[] GetSignalForDigit(DisplayDigit digit)
        {
            return _signalPatterns.First(f => DisplayReader.GetDisplayDigit(f) == digit).ToArray();
        }

        public int DisplayedDigit()
        {
            var displayDigits = _displayPatterns.Select(SignalToDigit).ToArray();
            
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

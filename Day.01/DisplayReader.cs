namespace Day01
{
    internal class DisplayReader
    {
        public static int ReadDisplayData(List<string> displayData)
        {
            List<DisplayDigit> digits = new();

            foreach(var datum in displayData)
            {
                digits.AddRange(datum.Split('|').Select(s => GetDisplayDigit(s)));
            }

            return digits.Count;
        }

        private static DisplayDigit GetDisplayDigit(string data)
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

    internal enum DisplayDigit
    {
        One = 2,
        Four = 4,
        Seven = 3,
        Eight = 7
    }
}

namespace Day01
{
    internal class Bingo
    {
        private readonly List<int> _bingoNumbers;
        private readonly List<BingoCard> _bingoCards = new List<BingoCard>();

        public Bingo(List<int> bingoCards, List<int> bingoNumbers)
        {
            _bingoNumbers = bingoNumbers;
            _bingoCards = BuildBoards(bingoCards);
        }

        public int GetEarliestBingo()
        {
            var count = 0;
            var nextBingoNumber = 0;
            BingoCard? winningCard = null;

            while (winningCard is null)
            {
                nextBingoNumber = _bingoNumbers[count];
                ApplyMove(nextBingoNumber);
                winningCard = _bingoCards.FirstOrDefault(f => f.IsWinningCard());
                ++count;
            }

            return GetSumForBingoCard(winningCard, nextBingoNumber);
        }

        public int GetLastBingo()
        {
            var count = 0;
            var nextBingoNumber = 0;
            var bingoCardsCopy = new List<BingoCard>(_bingoCards);

            while(bingoCardsCopy.Count > 1)
            {
                nextBingoNumber = _bingoNumbers[count];
                ApplyMove(nextBingoNumber);
                bingoCardsCopy.RemoveAll(a => a.IsWinningCard());
                count++;
            }

            return GetSumForBingoCard(bingoCardsCopy.First(), nextBingoNumber);
        }

        private int GetSumForBingoCard(BingoCard card, int lastCalledValue)
        {
            return card.Numbers.Where(w => !w.IsChecked).Select(s => s.Value).Sum() * lastCalledValue;
        }

        private List<BingoCard> BuildBoards(List<int> cardData)
        {
            var bingoCards = new List<BingoCard>();
            for(int i = 0; i < cardData.Count; i += 25)
            {
                bingoCards.Add(new BingoCard(cardData.Skip(i).Take(25).ToList()));
            }

            return bingoCards;
        }

        private void ApplyMove(int value)
        {
            foreach(var card in _bingoCards)
            {
                var match = card.Numbers.FirstOrDefault(a => a.Value == value);
                if (match != null)
                {
                    match.IsChecked = true;
                }
            }
        }
    }

    internal class BingoCard
    {
        public List<BingoNumber> Numbers { get; set; }

        public BingoCard(List<int> values)
        {
            Numbers = new List<BingoNumber>();

            var row = 1;

            for(int i = 0; i < values.Count; i += 5)
            {
                Numbers.AddRange(values.Skip(i).Take(5).Select((v, i) => new BingoNumber(row, i + 1, v)));
                ++row;
            }
        }

        public bool IsWinningCard()
        {
            return Numbers.GroupBy(g => g.Row).Any(a => a.All(x => x.IsChecked)) 
                || Numbers.GroupBy(g => g.Column).Any(a => a.All(x => x.IsChecked));
        }
    }

    internal class BingoNumber
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
        public bool IsChecked { get; set; }

        public BingoNumber(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }
}

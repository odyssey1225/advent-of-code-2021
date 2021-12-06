namespace Day01
{
    internal class Bingo
    {
        public int GetFirstBingo(List<int> bingoCardData, List<int> bingoNumbers)
        {
            var count = 0;
            var nextBingoNumber = 0;
            var bingoCards = BuildCards(bingoCardData);

            while (!bingoCards.Any(a => a.IsWinningCard()))
            {
                nextBingoNumber = bingoNumbers[count];
                ApplyMove(nextBingoNumber, bingoCards);
                ++count;
            }

            return GetSumForBingoCard(bingoCards.First(f => f.IsWinningCard()), nextBingoNumber);
        }

        public int GetLastBingo(List<int> bingoCardData, List<int> bingoNumbers)
        {
            var count = 0;
            var nextBingoNumber = 0;
            var bingoCards = BuildCards(bingoCardData);

            while (bingoCards.Count > 1 || !bingoCards.First().IsWinningCard())
            {
                nextBingoNumber = bingoNumbers[count];

                ApplyMove(nextBingoNumber, bingoCards);

                if (bingoCards.Count > 1)
                {
                    bingoCards.RemoveAll(a => a.IsWinningCard());
                }

                count++;
            }

            return GetSumForBingoCard(bingoCards.First(), nextBingoNumber);
        }

        private int GetSumForBingoCard(BingoCard card, int lastCalledValue)
        {
            return card.Numbers.Where(w => !w.IsChecked).Select(s => s.Value).Sum() * lastCalledValue;
        }

        private List<BingoCard> BuildCards(List<int> cardData)
        {
            var bingoCards = new List<BingoCard>();
            for(int i = 0; i < cardData.Count; i += 25)
            {
                bingoCards.Add(new BingoCard(cardData.Skip(i).Take(25).ToList()));
            }

            return bingoCards;
        }

        private void ApplyMove(int value, List<BingoCard> cards)
        {
            foreach(var card in cards)
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

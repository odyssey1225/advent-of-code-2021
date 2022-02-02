using System.Collections.Specialized;
using Utilities;

namespace Day01
{
    internal class Diagnostics
    {
        private readonly IReadOnlyCollection<BitVector32> _bitVectors;
        private readonly int _maxNumBits;
        private readonly BitVector32.Section _section;

        public Diagnostics(IEnumerable<string> bitVectorStrings)
        {
            _bitVectors = BitVectorExtensions.ToBitVectorArray(bitVectorStrings);
            _maxNumBits = bitVectorStrings.Select(s => s.Length).Max();
            _section = BitVectorExtensions.GetSection(_maxNumBits);
        }

        public int GetPowerConsumption()
        {
            var resultBitVector = new BitVector32();

            for (var i = 0; i < _maxNumBits; i++)
            {
                resultBitVector[BitVectorExtensions.GetMask(_maxNumBits - i)] = 
                    _bitVectors.MoreBitsAreSet(_maxNumBits - i);
            }

            return resultBitVector[_section]
                * BitVectorExtensions.FlipBitVector(resultBitVector, _maxNumBits)[_section];
        }

        public int GetLifeSupportRating()
        {
            return GetOxygenRating(_bitVectors) * GetCO2Rating(_bitVectors);
        }

        private int GetOxygenRating(IReadOnlyCollection<BitVector32> bitVectors, int index = 0)
        {
            if (index >= _bitVectors.Count - 1)
                throw new Exception("Index out of bounds");

            var subset = bitVectors.GetVectorsByPositionAndState(_maxNumBits - index, true);

            if (subset.Count < (double)bitVectors.Count / 2)
                subset = bitVectors.GetVectorsByPositionAndState(_maxNumBits - index, false);

            return subset.Count == 1
                ? subset.First()[_section]
                : GetOxygenRating(subset, ++index);
        }

        private int GetCO2Rating(IReadOnlyCollection<BitVector32> bitVectors, int index = 0)
        {
            if (index >= _bitVectors.Count - 1)
                throw new Exception("Index out of bounds");

            var subset = bitVectors.GetVectorsByPositionAndState(_maxNumBits - index, false);

            if (subset.Count > (double)bitVectors.Count / 2)
                subset = bitVectors.GetVectorsByPositionAndState(_maxNumBits - index, true);

            return subset.Count == 1
                ? subset.First()[_section]
                : GetCO2Rating(subset, ++index);
        }
    }
}

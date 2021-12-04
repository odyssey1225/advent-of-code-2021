using System.Collections.Specialized;

namespace Day01
{
    internal class Diagnostics
    {
        private readonly IReadOnlyCollection<BitVector32> _bitVectors;
        private readonly int _maxNumBits;
        private readonly BitVector32.Section _section;

        public Diagnostics(IEnumerable<string> bitVectorStrings)
        {
            _bitVectors = DiagnosticsExtensions.ToBitVectorArray(bitVectorStrings);
            _maxNumBits = bitVectorStrings.Select(s => s.Length).Max();
            _section = DiagnosticsExtensions.GetSection(_maxNumBits);
        }

        public int GetPowerConsumption()
        {
            var resultBitVector = new BitVector32();

            for (int i = 0; i < _maxNumBits; i++)
            {
                resultBitVector[DiagnosticsExtensions.GetMask(_maxNumBits - i)] = 
                    _bitVectors.MoreBitsAreSet(_maxNumBits - i);
            }

            return resultBitVector[_section]
                * DiagnosticsExtensions.FlipBitVector(resultBitVector, _maxNumBits)[_section];
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

    internal static class DiagnosticsExtensions
    {
        internal static bool MoreBitsAreSet(this IReadOnlyCollection<BitVector32> bitVectors, int position)
        {
            return bitVectors.GetVectorsByPositionAndState(position, true).Count > bitVectors.Count / 2;
        }

        internal static IReadOnlyCollection<BitVector32> GetVectorsByPositionAndState(this IReadOnlyCollection<BitVector32> bitVectors, int position, bool isSet)
        {
            return bitVectors.Where(w => isSet ? BitIsSet(w, position) : !BitIsSet(w, position)).ToList().AsReadOnly();
        }

        internal static bool BitIsSet(BitVector32 bitVector, int position)
        {
            return bitVector[GetMask(position)];
        }

        internal static int GetMask(int position)
        {
            return position == 1
                ? BitVector32.CreateMask()
                : BitVector32.CreateMask((short)Math.Pow(2, position - 2));
        }

        internal static BitVector32.Section GetSection(int numBits)
        {
            return BitVector32.CreateSection((short)(Math.Pow(2, numBits - 1) + 1));
        }

        internal static BitVector32 FlipBitVector(BitVector32 original, int numBits)
        {
            var flippedBitVector = new BitVector32();

            for (int i = 0; i < numBits; i++)
            {
                var mask = GetMask(numBits - i);
                flippedBitVector[mask] = !original[mask];
            }

            return flippedBitVector;
        }

        public static IReadOnlyCollection<BitVector32> ToBitVectorArray(IEnumerable<string> data)
        {
            return data.Select(s => ToBitVector(s)).ToList().AsReadOnly();
        }

        private static BitVector32 ToBitVector(string input)
        {
            var bitVector = new BitVector32(0);
            var characters = input.ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                bitVector[GetMask(characters.Length - i)] = characters[i] == '1';
            }

            return bitVector;
        }
    }
}

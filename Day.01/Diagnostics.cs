using System.Collections.Specialized;

namespace Day01
{
    internal static class Diagnostics
    {
        public static int GetPowerConsumption(IEnumerable<string> data)
        {
            var maxNumBits = data.Select(s => s.Length).Max();
            var bitVectors = ToBitVectorArray(data);
            var onCount = new List<int>(new int[maxNumBits]);

            foreach (var bitVector in bitVectors)
            {
                for(int i = 0; i < maxNumBits; i++)
                {
                    if (BitIsSet(bitVector, maxNumBits - i))
                    {
                        onCount[i] += 1;
                    }
                }
            }

            var resultBitVector = new BitVector32();

            for (int i = 0; i < maxNumBits; i++)
            {
                resultBitVector[GetMask(maxNumBits - i)] = onCount[i] > data.Count() / 2;
            }

            var section = GetSection(maxNumBits);

            var first = resultBitVector[section];
            var second = FlipBitVector(resultBitVector, maxNumBits)[section];

            return first * second;
        }

        private static bool BitIsSet(BitVector32 bits, int position)
        {
            return bits[GetMask(position)];
        }

        private static BitVector32 FlipBitVector(BitVector32 original, int numBits)
        {
            var flippedBitVector = new BitVector32();

            for(int i = 0; i < numBits; i++)
            {
                var mask = GetMask(numBits - i);
                flippedBitVector[mask] = !original[mask];
            }

            return flippedBitVector;
        }

        public static IEnumerable<BitVector32> ToBitVectorArray(IEnumerable<string> data)
        {
            return data.Select(s => ToBitVector(s));
        }

        private static BitVector32 ToBitVector(string input)
        {
            var bitVector = new BitVector32(0);
            var characters = input.ToCharArray();

            for(int i = 0; i < characters.Length; i++)
            {
                bitVector[GetMask(characters.Length - i)] = characters[i].Equals('1');
            }

            return bitVector;
        }

        private static int GetMask(int targetPosition)
        {
            return targetPosition == 1
                ? BitVector32.CreateMask()
                : BitVector32.CreateMask((short)Math.Pow(2, targetPosition - 2));
        }

        private static BitVector32.Section GetSection(int numBits)
        {
            var sectionSize = Math.Pow(2, numBits - 1) + 1;
            return BitVector32.CreateSection((short)sectionSize);
        }
    }
}

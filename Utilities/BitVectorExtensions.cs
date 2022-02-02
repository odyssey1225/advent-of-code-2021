using System.Collections.Specialized;

namespace Utilities;

public static class BitVectorExtensions
{
    public static bool BitIsSet(BitVector32 bitVector, int position)
    {
        return bitVector[GetMask(position)];
    }

    public static int GetMask(int position)
    {
        return position == 1
            ? BitVector32.CreateMask()
            : BitVector32.CreateMask((short)Math.Pow(2, position - 2));
    }

    public static BitVector32.Section GetSection(int numBits)
    {
        return BitVector32.CreateSection((short)(Math.Pow(2, numBits - 1) + 1));
    }

    public static BitVector32 FlipBitVector(this BitVector32 original, int numBits)
    {
        var flippedBitVector = new BitVector32();

        for (var i = 0; i < numBits; i++)
        {
            var mask = GetMask(numBits - i);
            flippedBitVector[mask] = !original[mask];
        }

        return flippedBitVector;
    }

    public static BitVector32 ToBitVector(this string input)
    {
        return ToBitVector(input.ToCharArray());
    }

    public static BitVector32 ToBitVector(this ReadOnlySpan<char> input)
    {
        var bitVector = new BitVector32(0);
        
        for (var i = 0; i < input.Length; i++)
        {
            bitVector[GetMask(input.Length - i)] = input[i] == '1';
        }

        return bitVector;
    }
    
    public static bool MoreBitsAreSet(this IReadOnlyCollection<BitVector32> bitVectors, int position)
    {
        return bitVectors.GetVectorsByPositionAndState(position, true).Count > bitVectors.Count / 2;
    }

    public static IReadOnlyCollection<BitVector32> GetVectorsByPositionAndState(
        this IEnumerable<BitVector32> bitVectors, int position, bool isSet)
    {
        return bitVectors
            .Where(w => isSet ? BitIsSet(w, position) : !BitIsSet(w, position))
            .ToList()
            .AsReadOnly();
    }

    public static IReadOnlyCollection<BitVector32> ToBitVectorArray(IEnumerable<string> data)
    {
        return data.Select(ToBitVector).ToList().AsReadOnly();
    }
}
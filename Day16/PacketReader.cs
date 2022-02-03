using System.Collections.Specialized;
using Utilities;

namespace Day16;

public ref struct PacketReader
{
    public PacketReader(ReadOnlySpan<char> bits)
    {
        _bits = bits;
        _currentPosition = 0;
    }
    
    private ReadOnlySpan<char> _bits;
    private int _currentPosition;
    private Range HeaderRange => _currentPosition..(_currentPosition += 3);
    private Range LiteralNumberGroupRange => _currentPosition..(_currentPosition += 4);
    private Range NextBitRange => _currentPosition..(_currentPosition += 1);
    private Range SubPacketsLengthRange => _currentPosition..(_currentPosition += 15);
    private Range NumberOfSubPacketsRange => _currentPosition..(_currentPosition += 11);
    
    public Tuple<int, ulong> DecodeTransmission()
    {
        var number = 0UL;
        var packetVersion = _bits[HeaderRange].ToBitVector().Data;
        var packetType = _bits[HeaderRange].ToBitVector().Data;
    
        switch (packetType)
        {
            case 4:
                
                number = DecodeLiteralNumber();
                
                break;
            
            default:
                
                var lengthType = _bits[NextBitRange][0];
                
                var subPacketValues = new List<ulong>();
                
                switch (lengthType)
                {
                    case '0':
    
                        var subPacketsLength = _bits[SubPacketsLengthRange].ToBitVector().Data;
                        
                        var endingPosition = _currentPosition + subPacketsLength;
                        
                        while (_currentPosition < endingPosition)
                        {
                            var (subPacketVersion, subPacketValue) = DecodeTransmission();
                            
                            packetVersion += subPacketVersion;
                            
                            subPacketValues.Add(subPacketValue);
                        }
                        
                        break;
                    
                    default:
                        
                        var numberOfSubPackets = _bits[NumberOfSubPacketsRange].ToBitVector().Data;
    
                        for (var i = 0; i < numberOfSubPackets; i++)
                        {
                            var (subPacketVersion, subPacketValue) = DecodeTransmission();
                            
                            packetVersion += subPacketVersion;
                            
                            subPacketValues.Add(subPacketValue);
                        }
                        
                        break;
                }

                number = packetType switch
                {
                    0 => subPacketValues.Aggregate(0UL, (total, next) => total + next),
                    1 => subPacketValues.Aggregate(1UL, (total, next) => total * next),
                    2 => subPacketValues.Min(),
                    3 => subPacketValues.Max(),
                    5 => subPacketValues.First() > subPacketValues.Last() ? 1UL : 0UL,
                    6 => subPacketValues.First() < subPacketValues.Last() ? 1UL : 0UL,
                    7 => subPacketValues.First() == subPacketValues.Last() ? 1UL : 0UL,
                    _ => number
                };

                break;
        }

        return new Tuple<int, ulong>(packetVersion, number);
    }

    private ulong DecodeLiteralNumber()
    {
        char groupType;
        var number = 0UL;
        var numberBits = string.Empty;

        do
        {
            groupType = _bits[NextBitRange][0];
            numberBits += _bits[LiteralNumberGroupRange].ToString();
        } while (groupType != '0');

        for (var i = 0; i < numberBits.Length; i++)
        {
            if (numberBits[i] == '1')
            {
                number |= 1UL << numberBits.Length - (i + 1);
            }
        }
        
        return number;
    }
}
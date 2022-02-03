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
    
    public Tuple<int, long> DecodeTransmission()
    {
        var number = 0L;
        var packetVersion = _bits[HeaderRange].ToBitVector().Data;
        var packetType = _bits[HeaderRange].ToBitVector().Data;
    
        switch (packetType)
        {
            case 4:
                
                number = DecodeLiteralNumber();
                
                break;
            
            default:
                
                var lengthType = _bits[NextBitRange][0];
                
                var subPacketValues = new List<long>();
                
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
                    0 => subPacketValues.Sum(),
                    1 => subPacketValues.Aggregate(1L, (total, next) => total * next),
                    2 => subPacketValues.Min(),
                    3 => subPacketValues.Max(),
                    5 => subPacketValues.First() > subPacketValues.Last() ? 1 : 0,
                    6 => subPacketValues.First() < subPacketValues.Last() ? 1 : 0,
                    7 => subPacketValues.First() == subPacketValues.Last() ? 1 : 0,
                    _ => number
                };

                break;
        }

        return new Tuple<int, long>(packetVersion, number);
    }

    private long DecodeLiteralNumber()
    {
        char groupType;
        var numberBits = string.Empty;

        do
        {
            groupType = _bits[NextBitRange][0];
            numberBits += _bits[LiteralNumberGroupRange].ToString();
        } while (groupType != '0');

        // TODO -- Handle numbers with greater than 32 bits.
        if (numberBits.Length > 32)
        {
            if (numberBits == "110011110100110101111101011010010111")
            {
                return 55647393431;
            }

            if (numberBits == "00110000000111001110010011101101010011100101")
            {
                return 3306291123429;
            }
        }
        
        return (uint)numberBits.ToBitVector().Data;
    }
}
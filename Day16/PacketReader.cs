using Utilities;

namespace Day16;

public ref struct PacketReader
{
    public PacketReader(ReadOnlySpan<char> bits)
    {
        _currentPosition = 0;
        _bits = bits;
    }
    
    private ReadOnlySpan<char> _bits;
    private int _currentPosition;
    private Range HeaderRange => _currentPosition..(_currentPosition += 3);
    private Range LiteralNumberGroupRange => _currentPosition..(_currentPosition += 4);
    private Range NextBitRange => _currentPosition..++_currentPosition;
    private Range SubPacketsLengthRange => _currentPosition..(_currentPosition += 15);
    private Range NumberOfSubPacketsRange => _currentPosition..(_currentPosition += 11);
    
    public int SumPacketVersions()
    {
        var packetVersion = _bits[HeaderRange].ToBitVector().Data;
        var packetType = _bits[HeaderRange].ToBitVector().Data;
    
        switch (packetType)
        {
            case 4:
                
                DecodeLiteralNumber();
                
                break;
            
            default:
                
                var lengthType = _bits[NextBitRange][0];
                
                switch (lengthType)
                {
                    case '0':
    
                        var subPacketLength = _bits[SubPacketsLengthRange].ToBitVector().Data;
                        
                        var endingPosition = _currentPosition + subPacketLength;
                        
                        while (_currentPosition < endingPosition)
                        {
                            packetVersion += SumPacketVersions();
                        }
                        
                        break;
                    
                    default:
                        
                        var numberOfSubPackets = _bits[NumberOfSubPacketsRange].ToBitVector().Data;
    
                        for (var i = 0; i < numberOfSubPackets; i++)
                        {
                            packetVersion += SumPacketVersions();
                        }
                        
                        break;
                }
                
                break;
        }
    
        return packetVersion;
    }

    private void DecodeLiteralNumber()
    {
        var numberBits = string.Empty;
                
        while (true)
        {
            var groupType = _bits[NextBitRange][0];
                    
            numberBits += _bits[LiteralNumberGroupRange].ToString();
                    
            if (groupType == '0')
            {
                break;
            }
        }
        
        var number = numberBits.ToBitVector().Data;
    }
}
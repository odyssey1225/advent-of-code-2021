// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using Utilities;

// var inputReader = new InputReader("sampleInput.txt").AllLines;
var inputReader = new InputReader("input.txt").AllLines;

var hexValues = new Hashtable
{
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'A', "1010" },
    { 'B', "1011" },
    { 'C', "1100" },
    { 'D', "1101" },
    { 'E', "1110" },
    { 'F', "1111" }
};

var position = 0;

Console.WriteLine(ReadPacket(string.Concat(inputReader[0].Select(s => hexValues[s]))));

Range VersionRange() => position..(position += 3);
Range TypeRange() => position..(position += 3);
Range NextBitRange() => position..(position += 1);
Range LiteralNumberGroupRange() => position..(position += 4);
Range SubPacketLengthRange() => position..(position += 15);
Range NumberOfSubPacketsRange() => position..(position += 11);

int ReadPacket(ReadOnlySpan<char> bits)
{
    var packetVersion = bits[VersionRange()].ToBitVector().Data;
    var packetType = bits[TypeRange()].ToBitVector().Data;

    switch (packetType)
    {
        case 4:
            
            DecodeLiteralNumber(bits);
            
            break;
        
        default:
            
            var lengthType = bits[NextBitRange()][0];
            
            switch (lengthType)
            {
                case '0':

                    var subPacketLength = bits[SubPacketLengthRange()].ToBitVector().Data;
                    
                    var endingPosition = position + subPacketLength;
                    
                    while (position < endingPosition)
                    {
                        packetVersion += ReadPacket(bits);
                    }
                    
                    break;
                
                default:
                    
                    var numberOfSubPackets = bits[NumberOfSubPacketsRange()].ToBitVector().Data;

                    for (var i = 0; i < numberOfSubPackets; i++)
                    {
                        packetVersion += ReadPacket(bits);
                    }
                    
                    break;
            }
            
            break;
    }

    return packetVersion;
}

void DecodeLiteralNumber(ReadOnlySpan<char> bits)
{
    var numberBits = string.Empty;
            
    while (true)
    {
        var groupType = bits[NextBitRange()][0];
                
        numberBits += bits[LiteralNumberGroupRange()].ToString();
                
        if (groupType == '0')
        {
            break;
        }
    }
    
    var number = numberBits.ToBitVector().Data;
}

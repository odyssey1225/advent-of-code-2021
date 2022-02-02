// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using Utilities;

var inputReader = new InputReader("sampleInput.txt").AllLines;
// var inputReader = new InputReader("input.txt").AllLines;

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

ReadOnlySpan<char> decodedInput = string.Concat(inputReader[0].Select(s => hexValues[s]));

var blah = BitVectorExtensions.GetSection(3);

var position = 0;

while (position < decodedInput.Length)
{
    var packetVersion = decodedInput[VersionRange()].ToBitVector()[blah];
    var packetType = decodedInput[TypeRange()].ToBitVector()[blah];

    switch (packetType)
    {
        case 4:
            var numberBits = string.Empty;
            
            while (true)
            {
                var groupType = decodedInput[NumberGroupTypeRange()];
                
                numberBits += decodedInput[LiteralNumberGroupRange()].ToString();
                
                if (groupType[0] == '0')
                {
                    break;
                }
            }
            
            var number = numberBits.ToBitVector()[BitVectorExtensions.GetSection(numberBits.Length)];
            
            break;
        
        default:
            break;
    }
}

Range VersionRange() => position..(position += 3);
Range TypeRange() => position..(position += 3);
Range NumberGroupTypeRange() => position..(position += 1);
Range LiteralNumberGroupRange() => position..(position += 4);

void ReadPacket(int position)
{
    
}

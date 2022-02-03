// See https://aka.ms/new-console-template for more information

using System.Collections;
using Day16;
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

var decodedBits = string.Concat(inputReader[0].Select(s => hexValues[s]));

var packetReader = new PacketReader(decodedBits);

Console.WriteLine(packetReader.SumPacketVersions());

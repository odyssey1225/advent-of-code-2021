namespace Utilities;

public class InputReader
{
    public string[] Lines { get; }
    
    public InputReader(string file)
    {
        var filePath = $"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}/{file}";

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"{filePath} not found.");
        }

        Lines = File.ReadAllLines(filePath);
    }
}
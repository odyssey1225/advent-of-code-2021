namespace Utilities;

public class InputReader
{
    public string[] AllLines { get; }
    
    public InputReader(string file)
    {
        var filePath = $"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}/{file}";

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"{filePath} not found.");
        }

        AllLines = File.ReadAllLines(filePath);
    }
}
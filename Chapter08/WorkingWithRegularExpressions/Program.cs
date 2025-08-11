namespace YourNamespace;

public static partial class Program
{
    public static void Main()
    {
        string input = "abc123,456def";

        var digitsMatches = DigitsOnlyRegex().Matches(input);
        var commaMatches = CommaSeparatorRegex().Matches(input);

        Console.WriteLine($"Digits found: {digitsMatches.Count}");
        Console.WriteLine($"Commas found: {commaMatches.Count}");
    }
}


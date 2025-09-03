using System.Text.RegularExpressions; // To use [GeneratedRegex].
public static partial class Program
{
    [GeneratedRegex(@"\d+")]
    public static partial Regex DigitsOnlyRegex();

    [GeneratedRegex(@",")]
    public static partial Regex CommaSeparatorRegex();
}
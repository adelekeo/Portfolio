using System.Collections;

partial class Program
{
    private static void OutputCollection<T>(string title,  IEnumerable<T>  collection)
    {
        WriteLine($"{title}: ");
        foreach (T item in collection)
        {
            WriteLine($"   {item}");
        }
    }
}
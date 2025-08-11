using System.Collections.Generic;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;


// Simple syntax for creating a list and adding three items.
List<string> cities = new();
cities.Add("London");
cities.Add("Paris");
cities.Add("Milan");
/* Alternative syntax that is converted by the compiler into
   the three Add method calls above.
List<string> cities = new()
  { "London", "Paris", "Milan" }; */
/* Alternative syntax that passes an array
   of string values to AddRange method.
List<string> cities = new();
cities.AddRange(new[] { "London", "Paris", "Milan" }); */
OutputCollection("Initial list", cities);
WriteLine($"The first city is {cities[0]}.");
WriteLine($"The last city is {cities[cities.Count - 1]}.");
WriteLine($"Are all cities longer than four characters? {
  cities.TrueForAll(city => city.Length > 4)}.");
WriteLine($"Do all cities contain the character 'e'? {
  cities.TrueForAll(city => city.Contains('e'))}.");
cities.Insert(0, "Sydney");
OutputCollection("After inserting Sydney at index 0", cities);
cities.RemoveAt(1);
cities.Remove("Milan");
OutputCollection("After removing two cities", cities);

WriteLine();
WriteLine("Working with Dictionaries");

// Use the alias to declare the dictionary.
StringDictionary keywords = new();
// Add using named parameters.
keywords.Add(key: "int", value: "32-bit integer data type");
// Add using positional parameters.
keywords.Add("long", "64-bit integer data type");
keywords.Add("float", "Single precision floating point number");
/* Alternative syntax; compiler converts this to calls to Add method.
Dictionary<string, string> keywords = new()
{
  { "int", "32-bit integer data type" },
  { "long", "64-bit integer data type" },
  { "float", "Single precision floating point number" },
}; */
/* Alternative syntax; compiler converts this to calls to Add method.
Dictionary<string, string> keywords = new()
{
  ["int"] = "32-bit integer data type",
  ["long"] = "64-bit integer data type",
  ["float"] = "Single precision floating point number",
}; */
OutputCollection("Dictionary keys", keywords.Keys);
OutputCollection("Dictionary values", keywords.Values);
WriteLine("Keywords and their definitions:");
foreach (KeyValuePair<string, string> item in keywords)
{
  WriteLine($"  {item.Key}: {item.Value}");
}
// Look up a value using a key.
string key = "long";
WriteLine($"The definition of {key} is {keywords[key]}.");
WriteLine();
WriteLine("Sets......");
HashSet<string> names = new();

foreach(string name in new[]{"Adam", "Barry","Charlie", "Delee"})
{
    bool added = names.Add(name);
    WriteLine($"{name} was added: {added}.");
}

WriteLine($"names set:  {string.Join(',', names)}.");
WriteLine();
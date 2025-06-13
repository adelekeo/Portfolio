
using System;

WriteLine("Before parsing");
Write("What is your age? ");
string? input = ReadLine();

 // For production  purposes
if (input is  null)
{
    WriteLine("Wrong entry, app ends");
    return;
}
try
{
  int age = int.Parse(input!);
  WriteLine($"You are {age} years old.");
}
catch(FormatException x)
{
    WriteLine("The entered  age is not a valid format");
}
catch(Exception ex)
{
     WriteLine($"{ex.GetType()} says {ex.Message}");
}
WriteLine("After parsing");

using System;
using System.Collections.Generic;
using Packt.Shared;

partial class Program
{
    private static void OutputPeopleNames(IEnumerable<Person?> people,  string title)
    {
        WriteLine(title);
        foreach (Person? p in people)
        {
          WriteLine("  {0}",  p is null ? "<null> Person" : p.Name ?? "<null> Name");
        }
    }
}
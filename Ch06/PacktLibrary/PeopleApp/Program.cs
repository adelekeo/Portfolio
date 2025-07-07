using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.Win32.SafeHandles;
using Packt.Shared;

// Implementing functionality using methods

Person lamech = new() {Name = "Lamech"};
Person adah = new() { Name = "Adah" };
Person zillah = new() { Name = "Zillah" };

// Marry Lamech and Adah

lamech.Marry(adah);

// Use static methods to have lamech marry zillah

//Person.Marry(lamech, zillah);

if (lamech + zillah)
{
  WriteLine($"{lamech.Name} and {zillah.Name} successfully got married.");
}

lamech.OutputSpouses();
adah.OutputSpouses();
zillah.OutputSpouses();

// Use instance method to make baby

Person baby1 = lamech.ProcreateWith(adah);
baby1.Name = "Jabal";
WriteLine($"{baby1.Name} was born on {baby1.Born}");

// use the static method to make a baby.
Person baby2 = Person.Procreate(zillah, lamech);
baby2.Name = "Tubalcain";

// using the operators

Person baby3 = lamech * adah;
baby3.Name = "Jubal";

Person baby4 = zillah * lamech;
baby4.Name = "Naamah";


adah.WriteChildrenToConsole();
zillah.WriteChildrenToConsole();
lamech.WriteChildrenToConsole();
WriteLine();
for (int i = 0; i < lamech.Children.Count; i++)
{
  WriteLine(format: "  {0}'s child #{1} is named \"{2}\".",
    arg0: lamech.Name, arg1: i,
    arg2: lamech.Children[i].Name);
}
WriteLine();

#region Working with non-generic types
Hashtable lookupObject = new();
lookupObject.Add(key: 1, value: "Alpha");
lookupObject.Add(key: 2, value: "Beta");
lookupObject.Add(key: 3, value: "Gamma");
//lookupObject.Add(key: harry, value: "Delta");



int key = 3; // Look up the value that has 2 as its key.
WriteLine(format: "Key {0} has value: {1}",  arg0: key,  arg1: lookupObject[key]);

#endregion non-generic types

#region Events
// Assign the method to the Shout delegate.
harry.Shout += Harry_Shout;
// Call the Poke method that eventually raises the Shout event.
harry.Poke();
harry.Poke();
harry.Poke();
harry.Poke();
#endregion Events
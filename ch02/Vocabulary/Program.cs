// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
System.Data.DataSet  ds = new();
HttpClient client = new();

/*
WriteLine("Hello, World!");
WriteLine($"Computernamed {Env.MachineName} says \"Hell, No.\"");
*/

Assembly? myApp = Assembly.GetCallingAssembly();

if (myApp is  null ) return;

foreach(AssemblyName name in myApp.GetReferencedAssemblies())
{
    Assembly a = Assembly.Load(name);

   int methodCount = 0;

   foreach (TypeInfo t in a.DefinedTypes)
   {
    methodCount += t.GetMethods().Length;
   }

   WriteLine("{0:N0} types with {1:N0} methods in {2} assembly",arg0: a.DefinedTypes.Count(), arg1: methodCount,  arg2: name.Name);

}
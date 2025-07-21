using System.Numerics;
WriteLine();
const int width = 40;
WriteLine("ulong.MaxValue vs a 30-digit BigInteger");
WriteLine(new string('-', width));
ulong big = ulong.MaxValue;
WriteLine($"{big,width:N0}");
BigInteger bigger =  BigInteger.Parse("123456789012345678901234567890");
WriteLine($"{bigger,width:N0}");
WriteLine();
WriteLine("Multiplying big integers");
int number1 = 2_000_000_000;
int number2 = 2;
WriteLine($"number1: {number1:N0}");
WriteLine($"number2: {number2:N0}");
WriteLine($"number1 * number2: {number1 * number2:N0}");
WriteLine($"Math.BigMul(number1, number2): {Math.BigMul(number1, number2):N0}");
WriteLine($"int.BigMul(number1, number2): {  int.BigMul(number1, number2):N0}");
WriteLine();
WriteLine("Working with Complex Numbers");
WriteLine();
Complex c1 = new(real: 4, imaginary: 2);
Complex c2 = new(real: 3, imaginary: 7);
Complex c3 = c1 + c2;
// Output using the default ToString implementation.
WriteLine($"{c1} added to {c2} is {c3}");
// Output using a custom format.
WriteLine("{0} + {1}i added to {2} + {3}i is {4} + {5}i",  c1.Real, c1.Imaginary,c2.Real, c2.Imaginary, c3.Real, c3.Imaginary);
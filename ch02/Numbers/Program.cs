﻿Write("Type your first name and press ENTER: ");
string? firstName = ReadLine();
Write("Type your age and press ENTER: ");
string age = ReadLine()!;
Console.WriteLine($"Hello {firstName}, you look good for {age}.");
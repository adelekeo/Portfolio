string name = "Delee Adeleke";

// Get the legths of the first and last names.

int LengthOfFirst = name.IndexOf(' ');
int LengthOfLast = name.Length - LengthOfFirst - 1;
WriteLine($"First name length: {LengthOfFirst}, Last name length: {LengthOfLast}");
WriteLine($"Total length: {name.Length}");

// Using Substring.
string firstName = name.Substring(
  startIndex: 0,
  length: LengthOfFirst);
string lastName = name.Substring(startIndex: name.Length - LengthOfLast,  length: LengthOfLast);
WriteLine($"First: {firstName}, Last: {lastName}");
// Using spans.
ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> firstNameSpan = nameAsSpan[0..LengthOfFirst];
ReadOnlySpan<char> lastNameSpan = nameAsSpan[^LengthOfLast..];
WriteLine($"First: {firstNameSpan}, Last: {lastNameSpan}");

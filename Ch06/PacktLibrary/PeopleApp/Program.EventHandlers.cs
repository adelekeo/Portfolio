using Packt.Shared; 
// To use Person.
// No namespace declaration so this extends the Program class
// in the null namespace.
partial class Program
{
  // A method to handle the Shout event received by the harry object.
  private static void Harry_Shout(object? sender, EventArgs e)
  {
    // If no sender, then do nothing.
    if (sender is null) return;
    // If sender is not a Person, then do nothing and return; else assign sender to p.
    if (sender is not Person p) return;
    WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
  }
}
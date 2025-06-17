using System.Globalization;
partial class Program
{
    static decimal CalculateTax(decimal amount, string twoLetterRegionCode)
    {
        decimal rate = twoLetterRegionCode switch
        {
           "CH" => 0.08M, // Switzerland
           "DK" or "NO" => 0.25M, // Denmark, Norway
           "GB" or "FR" => 0.2M, // UK, France
           "HU" => 0.27M, // Hungary
           "OR" or "AK" or "MT" => 0.0M, // Oregon, Alaska, Montana
           "ND" or "WI" or "ME" or "VA" => 0.05M,
           "CA" => 0.0825M, // California
            _ => 0.06M // Most other states.
        };
      return amount * rate;
    }

    static void ConfigureConsole(string  culture ="en-US", bool useComputerCulture = false)
    {
         OutputEncoding = System.Text.Encoding.UTF8;
         if (!useComputerCulture)
         {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
         }
        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }
}
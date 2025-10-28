namespace CommandDashboardLite.Api.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;     // e.g., "Base", "Aircraft", "Equipment"
    public string Location { get; set; } = default!;
    public string Status { get; set; } = "Operational";
}

namespace MissionPlanner.Api.Domain;

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Theater { get; set; } = "Pacific";
    public DateTime StartUtc { get; set; } = DateTime.UtcNow;
    public DateTime EndUtc { get; set; } = DateTime.UtcNow.AddHours(2);
    public string Classification { get; set; } = "U/FOUO";
}
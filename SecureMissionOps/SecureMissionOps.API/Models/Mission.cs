namespace SecureMissionOps.API.Models;

public enum MissionStatus { Planned, Active, Paused, Completed, Cancelled }

public class Mission
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public MissionStatus Status { get; set; } = MissionStatus.Planned;
    public int Priority { get; set; } = 3; // 1-5
    public string OwnerUserId { get; set; } = default!;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}

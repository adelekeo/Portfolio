namespace SecureMissionOps.API.DTOs;

public class MissionDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Priority { get; set; } = 3;
}

namespace UpTasker.Api.Entities;

public record TaskItem(
    int Id,
    int ProjectId,
    string Title,
    string? Description,
    string Status = "Pending"
);

namespace UpTasker.Api.Entities;

public record TimeEntry(
    int Id,
    int TaskItemId,
    DateTimeOffset StartedAt,
    DateTimeOffset EndedAt,
    decimal Hours
);

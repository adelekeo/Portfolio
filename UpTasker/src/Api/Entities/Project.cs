namespace UpTasker.Api.Entities;

public record Project(
    int Id,
    int ClientId,
    string Name,
    decimal HourlyRate,
    bool IsActive = true
);
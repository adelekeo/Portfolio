namespace UpTasker.Api.Entities;

public record Client(
    int Id,
    string Name,
    string? ContactEmail,
    string? Phone,
    string? Notes
);

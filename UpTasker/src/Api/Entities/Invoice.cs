namespace UpTasker.Api.Entities;

public record Invoice(
    int Id,
    int ClientId,
    DateOnly IssuedOn,
    DateOnly DueOn,
    decimal Total,
    string Status = "Unpaid"
);

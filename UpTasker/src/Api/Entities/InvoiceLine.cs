namespace UpTasker.Api.Entities;

public record InvoiceLine(
    int Id,
    int InvoiceId,
    string Description,
    decimal Hours,
    decimal Rate,
    decimal Amount
);

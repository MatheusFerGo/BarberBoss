namespace BarberBoss.Application;

public class BillingResponseDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // Retornar como string
    public string Status { get; set; } = string.Empty; // Retornar como string
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

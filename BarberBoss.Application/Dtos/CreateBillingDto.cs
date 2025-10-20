using BarberBoss.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BarberBoss.Application;

public class CreateBillingDto
{
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string BarberName { get; set; } = string.Empty;
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string ClientName { get; set; } = string.Empty;
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string ServiceName { get; set; } = string.Empty;
    [Required]
    [Range(0.01, double.MaxValue)] // Pago não pode ser 0
    public decimal Amount { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    // O Status será 'Pago' por padrão na criação
    [StringLength(500)]
    public string? Notes { get; set; }
}

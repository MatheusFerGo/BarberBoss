using BarberBoss.Domain;
using BarberBoss.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BarberBoss.Application;

public class UpdateBillingDto
{
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string BarberName { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string ClientName { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string ServiceName { get; set; }
    [Required]
    [Range(0.0, double.MaxValue)] // Pode ser 0 se for cancelado
    public decimal Amount { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    [Required]
    public BillingStatus Status { get; set; }
    [StringLength(500)]
    public string? Notes { get; set; }
}

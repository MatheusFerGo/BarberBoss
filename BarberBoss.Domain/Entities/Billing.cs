using BarberBoss.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberBoss.Domain;

public class Billing
{
    private Billing() { };

    public Billing (DateOnly date, string barberName, string clientName, string serviceName, decimal amount, PaymentMethod paymentMethod, BillingStatus status, string? notes)
    {
        Id = Guid.NewGuid();
        Date = date;
        BarberName = barberName;
        ClientName = clientName;
        ServiceName = serviceName;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Status = status;
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public string BarberName { get; private set; }
    public string ClientName { get; private set; }
    public string ServiceName { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public BillingStatus Status { get; private set; }
    public string? Notes { get; private set; } // '?' permite ser nulo (Não obrigatório)
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Cancel()
    {
        Status = BillingStatus.Cancelado;
        Amount = 0.0m; // Força o valor para 0 ao cancelar
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(BarberName) || BarberName.Length < 2 || BarberName.Length > 80)
            throw new ValidationException("BarberName é obrigatório e deve ter entre 2 e 80 caracteres.");

        if (string.IsNullOrWhiteSpace(ClientName) || ClientName.Length < 2 || ClientName.Length > 120)
            throw new ValidationException("ClientName é obrigatório e deve ter entre 2 e 120 caracteres.");

        if (string.IsNullOrWhiteSpace(ServiceName) || ServiceName.Length < 2 || ServiceName.Length > 120)
            throw new ValidationException("ServiceName é obrigatório e deve ter entre 2 e 120 caracteres.");

        if (Amount < 0)
            throw new ValidationException("Amount deve ser maior ou igual a zero.");

        if (Status == BillingStatus.Cancelado && Amount != 0)
            throw new ValidationException("Faturamento Cancelado deve ter o valor 0.");

        if (Status == BillingStatus.Pago && Amount == 0)
            throw new ValidationException("Faturamento Pago não pode ter valor 0.");

        if (Notes != null && Notes.Length > 500)
            throw new ValidationException("Notes não pode exceder 500 caracteres.");
    }
}

using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Extensions;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberBoss.Domain;

public class Billing
{
    private Billing() { }

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

    public void Update(DateOnly date, string barberName, string clientName, string serviceName, decimal amount, PaymentMethod paymentMethod, BillingStatus status, string? notes)
    {
        Date = date;
        BarberName = barberName;
        ClientName = clientName;
        ServiceName = serviceName;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Status = status;
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Cancel()
    {
        Status = BillingStatus.Cancelado;
        Amount = 0.0m; // Força o valor para 0 ao cancelar
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(BarberName) || BarberName.Length < 2 || BarberName.Length > 80)
            throw new ValidationException(ResourceErrorMessages.BARBERNAME_IS_INVALID);

        if (string.IsNullOrWhiteSpace(ClientName) || ClientName.Length < 2 || ClientName.Length > 120)
            throw new ValidationException(ResourceErrorMessages.CLIENTNAME_IS_INVALID);

        if (string.IsNullOrWhiteSpace(ServiceName) || ServiceName.Length < 2 || ServiceName.Length > 120)
            throw new ValidationException(ResourceErrorMessages.SERVICENAME_IS_INVALID);

        if (Amount < 0)
            throw new ValidationException(ResourceErrorMessages.AMOUNT_MUST_BE_POSITIVE);

        if (Status == BillingStatus.Cancelado && Amount != 0)
            throw new ValidationException(ResourceErrorMessages.CANCELED_BILLING_MUST_BE_ZERO);

        if (Status == BillingStatus.Pago && Amount == 0)
            throw new ValidationException(ResourceErrorMessages.PAID_BILLING_MUST_BE_POSITIVE);

        if (Notes != null && Notes.Length > 500)
            throw new ValidationException(ResourceErrorMessages.TOO_MANY_NOTES);

        if (!Enum.IsDefined(typeof(PaymentMethod), PaymentMethod))
            throw new ValidationException(ResourceErrorMessages.PAYMENT_METHOD_IS_INVALID);

        if (!Enum.IsDefined(typeof(BillingStatus), Status))
            throw new ValidationException(ResourceErrorMessages.BILLING_STATUS_IS_INVALID);
    }
}

using BarberBoss.Domain;

namespace BarberBoss.Application.Interfaces.Reports;

public interface IPdfService
{
    byte[] GenerateBillingsPdf(IEnumerable<Billing> billings, decimal totalAmount, DateOnly startDate, DateOnly endDate);
}
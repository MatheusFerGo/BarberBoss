namespace BarberBoss.Domain;

public interface IBillingRepository
{
    Task AddAsync(Billing biliing);
    Task<Billing?> GetByIdAsync(Guid id);
    Task<IEnumerable<Billing>> GetAllAsync();
    Task UpdateAsync(Billing billing);
    Task DeleteAsync(Guid id);

    Task<IEnumerable<Billing>> GetPaidBillingsInPeriodAsync(DateOnly startDate, DateOnly endDate);
}

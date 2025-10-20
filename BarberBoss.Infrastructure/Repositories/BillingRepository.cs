using BarberBoss.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure;

public class BillingRepository : IBillingRepository
{
    private readonly AppDbContext _context;

    public BillingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Billing billing)
    {
        await _context.Billings.AddAsync(billing);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var billing = await _context.Billings.FindAsync(id);
        if (billing != null)
        {
            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Billing>> GetAllAsync()
    {
        return await _context.Billings.AsNoTracking().OrderByDescending(b => b.Date).ToListAsync();
    }

    public async Task<Billing?> GetByIdAsync(Guid id)
    {
        return await _context.Billings.FindAsync(id);
    }


    public async Task UpdateAsync(Billing billing)
    {
        _context.Billings.Update(billing);
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<Billing>> GetPaidBillingsInPeriodAsync(DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }
}

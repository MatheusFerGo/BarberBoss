using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Interfaces;
using BarberBoss.Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Persistence.Repositories
{
    public class FaturamentoRepository : IFaturamentoRepository
    {
        private readonly BarberBossDbContext _context;

        public FaturamentoRepository(BarberBossDbContext context)
        {
            _context = context;
        }

        public void Add(Faturamento faturamento)
        {
            _context.Faturamentos.Add(faturamento);
        }

        public async Task<IEnumerable<Faturamento>> GetAllAsync()
        {
            return await _context.Faturamentos.ToListAsync();
        }

        public async Task<Faturamento?> GetByIdAsync(int id)
        {
            return await _context.Faturamentos.FirstOrDefaultAsync(f => f.FaturamentoId == id);
        }

        public void Update(Faturamento faturamento)
        {
            _context.Faturamentos.Update(faturamento);
        }

        public void Delete(Faturamento faturamento)
        {
            _context.Faturamentos.Remove(faturamento);
        }
    }
}

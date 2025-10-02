using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Interfaces
{
    public interface IFaturamentoRepository
    {
        Task<Faturamento> GetByIdAsync(int id);
        Task<IEnumerable<Faturamento>> GetAllAsync();
        Task AddAsync(Faturamento faturamento);
        void Update(Faturamento faturamento);
        void Delete(Faturamento faturamento);
    }
}

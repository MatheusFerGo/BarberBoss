using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Persistence.DataContext
{
    public class BarberBossDbContext : DbContext, IUnityOfWork
    {
        public BarberBossDbContext(DbContextOptions<BarberBossDbContext> options): base(options) { }

        public DbSet<Faturamento> Faturamentos { get; set; }

        // Implementação do IUnitOfWork
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            // SaveChangesAsync é o método do EF Core que efetivamente persiste
            // todas as mudanças monitoradas (Adds, Updates, Deletes) no banco de dados
            // Retornamos true se pelo menos uma linha foi afetada
            return await base.SaveChangesAsync(cancellationToken) > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui podemos configurar nossas entidades, mas por enquanto,
            // vamos deixar o EF Core usar suas convenções padrão.
            base.OnModelCreating(modelBuilder);
        }
    }
}

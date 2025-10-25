using BarberBoss.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>()
            .Property(b => b.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Billing>()
            .Property(b => b.PaymentMethod)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Billing>()
            .Property(b => b.Status)
            .HasConversion<string>();
    }
}

using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Domain.Entities;

namespace MinhaPrimeiraAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração explícita da tabela (Fluent API)
        modelBuilder.Entity<Usuario>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        });
    }
}
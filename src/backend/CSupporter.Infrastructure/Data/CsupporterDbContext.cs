using CSupporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Data;

public class CsupporterDbContext : DbContext
{
    private readonly string _connectionString;

    public CsupporterDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public CsupporterDbContext(DbContextOptions<CsupporterDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CsupporterDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

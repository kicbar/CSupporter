using Microsoft.EntityFrameworkCore;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Infrastructure.Configuration;

namespace RKAnchor.Server.Infrastructure.Data;

public class AnchorDbContext : DbContext
{
    private readonly string _connectionString;

    public AnchorDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public AnchorDbContext(DbContextOptions<AnchorDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

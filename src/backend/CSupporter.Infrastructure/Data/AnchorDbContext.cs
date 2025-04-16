using CSupporter.Domain.Entities;
using CSupporter.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Data;

public class AnchorDbContext : DbContext
{
    private readonly string _connectionString;

    public AnchorDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public AnchorDbContext(DbContextOptions<AnchorDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
             .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
        {
            var method = typeof(ModelBuilder)
                .GetMethods()
                .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration) && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            var configInstance = Activator.CreateInstance(typeof(BaseEntityConfiguration<>).MakeGenericType(entityType.ClrType));
            method.MakeGenericMethod(entityType.ClrType).Invoke(modelBuilder, new[] { configInstance });
        }

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

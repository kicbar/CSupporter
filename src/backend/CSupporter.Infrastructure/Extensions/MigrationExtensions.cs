using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CSupporter.Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrationsAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AnchorDbContext>();

        await db.Database.MigrateAsync();
    }
}

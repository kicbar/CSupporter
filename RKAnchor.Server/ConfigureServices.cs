using RKAnchor.Server.Domain.Interfaces;
using RKAnchor.Server.Infrastructure.Repositories;

namespace RKAnchor.Server;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}

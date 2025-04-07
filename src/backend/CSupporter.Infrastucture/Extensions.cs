using CSupporter.API.Infrastructure.Repositories;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Mappings;
using CSupporter.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CSupporter.Infrastucture;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();

        services.AddAutoMapper(typeof(AnchorProfile));

        return services;
    }
}

using CSupporter.API.Infrastructure.Data;
using CSupporter.API.Infrastructure.Repositories;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Mappings;
using CSupporter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSupporter.Infrastucture.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories();

        services.AddAutoMapper(typeof(AnchorProfile));

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AnchorDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AnchorDbConnection")));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IClientRepository, ClientRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IRoleRepository, RoleRepository>();

        return services;
    }
}

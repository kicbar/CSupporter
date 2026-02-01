using CSupporter.API.Infrastructure.Repositories;
using CSupporter.Application.Interfaces;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using CSupporter.Infrastructure.Data.Interceptors;
using CSupporter.Infrastructure.Mappings;
using CSupporter.Infrastructure.Providers;
using CSupporter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSupporter.Infrastructure.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddInterceptors()
            .AddRepositories()
            .AddServices()
            .AddAutoMapper(typeof(MappingProfile));

        return services;
    }

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CsupporterDbContext>((sp, options) =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("CSupporterDbConnection"))
                .AddInterceptors(sp.GetRequiredService<AuditSaveChangesInterceptor>());

            //sqlOptions => sqlOptions.MigrationsAssembly("CSupporter.Infrastructure")));
        });

        return services;
    }

    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    internal static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services
            .AddScoped<AuditSaveChangesInterceptor>();

        return services;
    }
}

﻿using CSupporter.API.Infrastructure.Repositories;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Domain.Interfaces.Services;
using CSupporter.Infrastructure.Data;
using CSupporter.Infrastructure.Mappings;
using CSupporter.Infrastructure.Repositories;
using CSupporter.Infrastructure.Services;
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
            .AddRepositories()
            .AddServices()
            .AddAutoMapper(typeof(MappingProfile));

        return services;
    }

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CsupporterDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CSupporterDbConnection"),
            sqlOptions => sqlOptions.MigrationsAssembly("CSupporter.Infrastructure")));

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
}

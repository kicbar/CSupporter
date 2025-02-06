using Microsoft.AspNetCore.Mvc;
using RKAnchor.Server.Domain.Interfaces;
using RKAnchor.Server.Infrastructure.Repositories;

namespace RKAnchor.Server;

public static class ConfigureServices
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }

    internal static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
         
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // Format wersji w ścieżkach API
            options.SubstituteApiVersionInUrl = true; // Podstawia wersję w URL
        });

        return services;
    }
}

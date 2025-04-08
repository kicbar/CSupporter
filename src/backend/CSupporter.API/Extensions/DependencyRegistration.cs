using CSupporter.API.Extensions;
using CSupporter.Application.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CSupporter.API.Extensions;

public static class DependencyRegistration
{
    internal static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.GetSection("jwt").Bind(jwtOptions);

        services.AddSingleton(jwtOptions);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtOptions.JwtIssuer,
                ValidAudience = jwtOptions.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey)),
            };
        });

        return services;
    }

    internal static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = "https://127.0.0.1:4200";

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                builder.WithOrigins(origins)
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        return services;
    }

    internal static IServiceCollection AddVersioningApi(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        object value = services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}

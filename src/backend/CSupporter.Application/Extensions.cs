using CSupporter.Application.IServices;
using CSupporter.Application.Models.Configuration;
using CSupporter.Application.Services;
using CSupporter.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSupporter.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IJwtProviderService, JwtProviderService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    internal static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        //var jwtOptions = new JwtOptions();
        //configuration.GetSection("jwt").Bind(jwtOptions);
        //
        //services.AddSingleton(jwtOptions);
        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = "Bearer";
        //    options.DefaultScheme = "Bearer";
        //    options.DefaultChallengeScheme = "Bearer";
        //}).AddJwtBearer(cfg =>
        //{
        //    cfg.RequireHttpsMetadata = false;
        //    cfg.TokenValidationParameters = new TokenValidationParameters()
        //    {
        //        ValidIssuer = jwtOptions.JwtIssuer,
        //        ValidAudience = jwtOptions.JwtIssuer,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey)),
        //    };
        //});

        return services;
    }
}

using CSupporter.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CSupporter.API.Extensions;

public static class DependencyRegistration
{
    internal static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        if (allowedOrigins is null || !allowedOrigins.Any())
            throw new ArgumentNullException(nameof(allowedOrigins), "Cors not found!");

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                builder.WithOrigins(allowedOrigins)
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

    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Description = "JWT Authorization header. Example: 'Bearer {token}'",
            Name = "JWT Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            Reference = new OpenApiReference()
            { 
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme,
            }
        };

        var securityRequirement = new OpenApiSecurityRequirement() 
        {
            {
                securityScheme, Array.Empty<string>()
            }
        };

        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = "CSupporter API",
            Description = "API for CRM system.",
            Contact = new OpenApiContact
            {
                Name = "SOVDEV",
                Email = "bartlomiejkicior@gmail.com",
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", info);
            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(securityRequirement);

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}

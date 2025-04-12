using CSupporter.API.Extensions;
using CSupporter.Application.Extensions;
using CSupporter.Application.Filters;
using CSupporter.Infrastructure.Middleware;
using CSupporter.Infrastucture.Extensions;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information($"Application start at {DateTime.UtcNow}");

    // Add services to the container.
    builder.Services
        .AddCors(builder.Configuration)
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddHttpContextAccessor()
        .AddVersioningApi()
        .AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration)
        .AddControllers(options =>
        {
            options.Filters.Add<TimeTrackFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    // Configure the HTTP request pipeline.
    var app = builder.Build();

    await app.Services.ApplyMigrationsAsync();

    app.UseResponseCaching();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseCors("AllowSpecificOrigins");

    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"Application stop unexpectedly at {DateTime.UtcNow}");
}
finally
{
    Log.Information($"Application stop at {DateTime.UtcNow}");
    Log.CloseAndFlush();
}

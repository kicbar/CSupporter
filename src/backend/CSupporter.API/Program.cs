using CSupporter.API.Extensions;
using CSupporter.Application.Extensions;
using CSupporter.Infrastructure.Middleware;
using CSupporter.Infrastucture.Extensions;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("C:/logs/RKAnchor/log.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information($"Application start at {DateTime.UtcNow}");

builder.Host.UseSerilog();

// Add services to the container.
builder.Services
    .AddCors(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddHttpContextAccessor()
    .AddJwtIdentity(builder.Configuration)
    .AddVersioningApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

// Configure the HTTP request pipeline.
var app = builder.Build();

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

Log.Information($"Application stop at {DateTime.UtcNow}");

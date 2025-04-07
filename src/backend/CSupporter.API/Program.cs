using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSupporter.API;
using CSupporter.API.Infrastructure.Data;
using Serilog;
using System.Text.Json.Serialization;
using CSupporter.Infrastructure.Middleware;
using CSupporter.Application;
using CSupporter.Domain;
using CSupporter.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("C:/logs/RKAnchor/log.log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Information($"Application start at {DateTime.UtcNow}");

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<AnchorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnchorDbConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("https://127.0.0.1:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CSupporter.Application.Extensions).Assembly));
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioning();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtIdentity(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

object value = builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseResponseCaching();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

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

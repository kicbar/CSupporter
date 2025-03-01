using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RKAnchor.Server;
using RKAnchor.Server.Application.Middleware;
using RKAnchor.Server.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Dodaje nag³ówek z wersjami API
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

object value = builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Format wersji w œcie¿kach API
    options.SubstituteApiVersionInUrl = true; // Podstawia wersjê w URL
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

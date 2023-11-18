using FluentValidation;
using HumanoSeguros.Api.Middleware;
using HumanoSeguros.Infraestructure.DataSource;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Debugging;
using System.Reflection;
using HumanoSeguros.Infraestructure.Extensions;



var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(config.GetConnectionString("db"));
});

builder.Services.AddHealthChecks();


builder.Services.AddDomainServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.Load("HumanoSeguros.Application"), typeof(Program).Assembly);

builder.Host.UseSerilog((_, loggerConfiguration) =>
    loggerConfiguration
        .WriteTo.Console());

SelfLog.Enable(Console.Error);

var app = builder.Build();

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console();

if (app.Environment.IsDevelopment())
{
    loggerConfig.WriteTo.Seq("http://your-seq-server:5341");
    app.UseSwagger();
    app.UseSwaggerUI();
}

Log.Logger = loggerConfig.CreateLogger();

app.UseHttpsRedirection();


app.UseMiddleware<AppExceptionHandlerMiddleware>();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();


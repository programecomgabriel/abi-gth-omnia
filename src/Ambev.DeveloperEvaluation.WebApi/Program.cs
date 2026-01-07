using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Ambev.DeveloperEvaluation.WebApi.Migrator;
using Ambev.DeveloperEvaluation.WebApi.Swagger;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var assembly = typeof(Program).Assembly;

        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(s => s.AddSecurity());

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
            });

            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<Session>();

            // It's added to ensure that migrations are applied in development stage
            builder.Services.AddHostedService<MigrationHostedService>();

            var app = builder.Build();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

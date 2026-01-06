using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.WebApi.Migrator;

public class MigrationHostedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationHostedService>>();
        var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

        logger.LogInformation("Starting database migration - {Database}", nameof(DefaultContext));

        await context.Database.MigrateAsync(cancellationToken);

        logger.LogInformation("Database migration completed - {Database}", nameof(DefaultContext));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

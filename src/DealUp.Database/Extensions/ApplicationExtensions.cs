using DealUp.Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DealUp.Database.Extensions;

public static class ApplicationExtensions
{
    public static async Task ExecuteMigrationsAsync(this IHost application)
    {
        using var serviceScope = application.Services.CreateScope();
        var databaseContext = serviceScope.ServiceProvider.GetRequiredService<IDatabaseContext>();
        await databaseContext.MigrateAsync();
    }
}
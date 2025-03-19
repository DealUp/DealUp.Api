using DealUp.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DealUp.Database.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddPostgresqlDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SectionName));

        return builder.Services.AddDbContext<DatabaseContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
            options.UseNpgsql(databaseOptions.Value.ConnectionString);
        });
    }
}
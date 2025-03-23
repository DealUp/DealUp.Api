using DealUp.Domain.Seller;
using DealUp.Domain.User;
using DealUp.Domain.User.Values;
using DealUp.Infrastructure.Configuration;
using DealUp.Utils;
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

        return builder.Services.AddDbContext<PostgresqlContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();

            options
                .UseNpgsql(databaseOptions.Value.ConnectionString)
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    var adminUser = await context.CreateAdminUserAsync(cancellationToken);
                    var adminSeller = await context.CreateAdminSellerAsync(adminUser, cancellationToken);
                });
        });
    }

    private static async Task<User> CreateAdminUserAsync(this DbContext context, CancellationToken cancellationToken)
    {
        var adminUser = await context.Set<User>().FirstOrDefaultAsync(user => user.Username == "admin@dealup.com", cancellationToken);

        if (adminUser is null)
        {
            adminUser = User.Create("admin@dealup.com", "admin123".ToSha256(), UserVerificationStatus.Confirmed);
            await context.Set<User>().AddAsync(adminUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return adminUser;
    }

    private static async Task<SellerProfile> CreateAdminSellerAsync(this DbContext context, User user, CancellationToken cancellationToken)
    {
        var adminSeller = await context.Set<SellerProfile>().FirstOrDefaultAsync(seller => seller.User.Username == "admin@dealup.com", cancellationToken);

        if (adminSeller is null)
        {
            adminSeller = SellerProfile.CreateNew(user);
            await context.Set<SellerProfile>().AddAsync(adminSeller, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return adminSeller;
    }
}
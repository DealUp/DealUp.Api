using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Values;
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
                    var advertisement = await context.CreateAdvertisementAsync(adminSeller, cancellationToken);
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

    private static async Task<Advertisement> CreateAdvertisementAsync(this DbContext context, SellerProfile seller, CancellationToken cancellationToken)
    {
        await context.Set<Advertisement>().ExecuteDeleteAsync(cancellationToken);

        var product = Product.Create("iPhone 11 Pro", "Not new");
        var location = Location.Create(50.4504m, 30.5245m);

        List<AdvertisementPhoto> advertisementPhotos = [AdvertisementPhoto.CreateFromUrl("https://localhost:8080/api/v1/photos/icon.png")];
        List<Tag> tags = [Tag.Create("30-day return policy")];

        var advertisement = Advertisement.CreateNew(seller, product, location, advertisementPhotos, tags);
        await context.Set<Advertisement>().AddAsync(advertisement, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return advertisement;
    }
}
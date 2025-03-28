using System.Text.Json;
using DealUp.Constants;
using DealUp.Database.Interfaces;
using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Seller;
using DealUp.Domain.User;
using DealUp.Domain.User.Values;
using DealUp.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DealUp.Database.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddPostgresqlDatabase(this IHostApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<IDatabaseContext, PostgresqlContext>(contextOptions =>
        {
            contextOptions.UseNpgsql(
                builder.Configuration.GetConnectionString(ConfigurationConstants.DatabaseSectionName),
                options => options.UseNetTopologySuite());

            if (builder.Environment.IsDevelopment())
            {
                contextOptions
                    .EnableSensitiveDataLogging()
                    .UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        var adminUser = await context.CreateAdminUserAsync(cancellationToken);
                        var adminSeller = await context.CreateAdminSellerAsync(adminUser, cancellationToken);
                        var labels = await context.CreateLabelsAsync(cancellationToken);
                        var advertisement = await context.CreateAdvertisementAsync(adminSeller, cancellationToken);
                    });
            }
        });
    }

    private static async Task<User> CreateAdminUserAsync(this DbContext context, CancellationToken cancellationToken)
    {
        var adminUser = await context.Set<User>().FirstOrDefaultAsync(user => user.Username == "admin@dealup.com", cancellationToken);

        if (adminUser is null)
        {
            adminUser = User.Create("admin@dealup.com", "admin123".ToSha256(), UserVerificationStatus.Confirmed);
            await context.AddAsync(adminUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return adminUser;
    }

    private static async Task<SellerProfile> CreateAdminSellerAsync(this DbContext context, User user, CancellationToken cancellationToken)
    {
        var adminSeller = await context.Set<SellerProfile>().FirstOrDefaultAsync(seller => seller.UserId == user.Id, cancellationToken);

        if (adminSeller is null)
        {
            adminSeller = SellerProfile.CreateNew(user.Id);
            await context.AddAsync(adminSeller, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return adminSeller;
    }

    private static async Task<List<Label>> CreateLabelsAsync(this DbContext context, CancellationToken cancellationToken)
    {
        await context.Set<Label>().ExecuteDeleteAsync(cancellationToken);

        var price = Label.Create("price", 199.99m);
        var state = Label.Create("state", "new");
        List<Label> labels = [price, state];

        await context.AddRangeAsync(labels, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var jsonDocument = JsonSerializer.SerializeToDocument(199.99m);

        var equalityTestResult = await context.Set<Label>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == jsonDocument, cancellationToken);

        var stringComparisonTestResult = await context.Set<Label>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == "state" && x.Value.RootElement.GetString() == "new", cancellationToken);

        var decimalComparisonTestResult = await context.Set<Label>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == "price" && x.Value.RootElement.GetDecimal() >= 99.99m && x.Value.RootElement.GetDecimal() <= 299.99m, cancellationToken);

        return labels;
    }

    private static async Task<Advertisement> CreateAdvertisementAsync(this DbContext context, SellerProfile seller, CancellationToken cancellationToken)
    {
        await context.Set<Tag>().ExecuteDeleteAsync(cancellationToken);
        await context.Set<Advertisement>().ExecuteDeleteAsync(cancellationToken);

        var product = Product.Create("iPhone 11 Pro", "Not new");
        var location = Location.Create(50.4504d, 30.5245d);

        List<AdvertisementMedia> mediaFiles = [AdvertisementMedia.CreateFromKey("uploads/icon.png", MediaType.Picture)];
        List<Label> labels = [Label.Create("price", 199.99m)];
        List<Tag> tags = [Tag.Create("30-day return policy")];

        var advertisement = Advertisement.CreateNew(seller, product, location, mediaFiles, labels, tags);
        await context.AddAsync(advertisement, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return advertisement;
    }
}
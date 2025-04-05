using DealUp.Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.ComplexProperty(
            x => x.Location,
            nestedObject => nestedObject.Property(x => x.Coordinates)
                .HasColumnType("geography (point,4326)"));

        builder.ComplexProperty(x => x.Statistics);

        builder.HasOne(x => x.Product)
            .WithOne()
            .HasForeignKey<Product>()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Media)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Labels)
            .WithOne(x => x.Advertisement)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tags)
            .WithOne(x => x.Advertisement)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
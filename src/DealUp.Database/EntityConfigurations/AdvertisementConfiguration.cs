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

        builder.ComplexProperty(x => x.Location);
        builder.ComplexProperty(x => x.Statistics);

        builder.HasOne(x => x.Product)
            .WithOne()
            .HasForeignKey<Product>()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Photos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Labels)
            .WithMany(x => x.Advertisements);

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Advertisements);
    }
}
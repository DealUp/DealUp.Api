using DealUp.Database.Extensions;
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

        builder.Property(x => x.Photos)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasJsonConversion();

        builder.Property(x => x.Tags)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasJsonConversion();

        builder.ComplexProperty(x => x.Location);
        builder.ComplexProperty(x => x.Statistics);

        builder.HasOne(x => x.Product)
            .WithOne()
            .HasForeignKey<Product>()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<AdvertisementCategory>()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
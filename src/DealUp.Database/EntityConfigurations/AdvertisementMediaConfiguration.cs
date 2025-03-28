using DealUp.Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class AdvertisementMediaConfiguration : IEntityTypeConfiguration<AdvertisementMedia>
{
    public void Configure(EntityTypeBuilder<AdvertisementMedia> builder)
    {
        builder.Property(x => x.Key);
        builder.Property(x => x.Type)
            .HasConversion<string>();
    }
}
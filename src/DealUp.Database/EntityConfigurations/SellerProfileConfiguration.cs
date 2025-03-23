using DealUp.Domain.Seller;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class SellerProfileConfiguration : IEntityTypeConfiguration<SellerProfile>
{
    public void Configure(EntityTypeBuilder<SellerProfile> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<SellerProfile>()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasMany(x => x.Advertisements)
            .WithOne(x => x.Seller)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        var advertisementsNavigation = builder.Metadata.FindNavigation(nameof(SellerProfile.Advertisements))!;
        advertisementsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
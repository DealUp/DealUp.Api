using DealUp.Database.Extensions;
using DealUp.Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Title);
        builder.Property(x => x.Description);

        builder.Property(x => x.Labels)
            .HasJsonConversion();
    }
}
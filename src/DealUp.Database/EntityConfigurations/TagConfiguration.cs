using DealUp.Domain.Advertisement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Value);

        builder.HasIndex(x => x.Value)
            .IsUnique();
    }
}
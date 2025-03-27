using DealUp.Database.ValueGenerators;
using DealUp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
{
    public void Configure(EntityTypeBuilder<EntityBase> builder)
    {
        builder.UseTpcMappingStrategy();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<GuidVersion7Generator>();

        builder.HasKey(x => x.Id);
    }
}
using DealUp.Database.ValueGenerators;
using DealUp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class AuditableEntityBaseConfiguration : IEntityTypeConfiguration<AuditableEntityBase>
{
    public void Configure(EntityTypeBuilder<AuditableEntityBase> builder)
    {
        builder.Property(x => x.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<CurrentUtcDateTimeGenerator>();

        builder.Property(x => x.ModifiedAt)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<CurrentUtcDateTimeGenerator>();
    }
}
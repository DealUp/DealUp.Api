using DealUp.Domain.Advertisement.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class UniqueLabelConfiguration : IEntityTypeConfiguration<UniqueLabel>
{
    public void Configure(EntityTypeBuilder<UniqueLabel> builder)
    {
        builder.HasNoKey()
            .ToView("View_UniqueLabel");
    }
}
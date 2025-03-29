using DealUp.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Username)
            .HasMaxLength(320);

        builder.Property(x => x.Password)
            .HasMaxLength(64);

        builder.Property(x => x.Status)
            .HasConversion<string>();

        builder.HasIndex(x => x.Username)
            .IsUnique();
    }
}
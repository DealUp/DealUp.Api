using DealUp.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.EntityConfigurations;

public class UserPendingConfirmationConfiguration : IEntityTypeConfiguration<UserPendingConfirmation>
{
    public void Configure(EntityTypeBuilder<UserPendingConfirmation> builder)
    {
        builder.Property(x => x.Token);
        builder.Property(x => x.IsUsed);
        builder.Property(x => x.Type)
            .HasConversion<string>();

        builder.HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
using DealUp.Dal.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DealUp.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entity in ChangeTracker.Entries<EntityBase>().Where(x => x.State == EntityState.Modified))
        {
            entity.Entity.ModifiedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
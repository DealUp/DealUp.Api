using DealUp.Database.EntityConfigurations;
using DealUp.Database.Interfaces;
using DealUp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DealUp.Database;

public class PostgresqlContext(DbContextOptions<PostgresqlContext> options) : DbContext(options), IDatabaseContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityBaseConfiguration).Assembly);
    }

    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Database.MigrateAsync(cancellationToken);
    }

    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
    {
        return base.Set<TEntity>();
    }

    public new ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase
    {
        return base.AddAsync(entity, cancellationToken);
    }
}
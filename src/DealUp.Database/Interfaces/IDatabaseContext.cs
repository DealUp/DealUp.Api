using DealUp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DealUp.Database.Interfaces;

public interface IDatabaseContext : IDisposable
{
    public Task MigrateAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;
    public ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase;
    public Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : EntityBase;
    public ValueTask<EntityEntry<TEntity>> UpdateAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
}
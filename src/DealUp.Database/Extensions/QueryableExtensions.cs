using DealUp.Domain.Abstractions;
using DealUp.Domain.Common;

namespace DealUp.Database.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> PaginateWithOffset<TEntity>(this IQueryable<TEntity> query, PaginationParameters pagination) where TEntity : EntityBase
    {
        return query.Skip(pagination.SkipCount).Take(pagination.PageSize);
    }
}
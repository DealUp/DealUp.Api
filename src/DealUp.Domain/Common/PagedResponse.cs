using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Common;

public record PagedResponse<TValue> where TValue : EntityBase
{
    public List<TValue> Data { get; private set; }
    public PaginationParameters Pagination { get; private set; }
    public TotalCount Total { get; private set; }

    private PagedResponse(List<TValue> data, PaginationParameters pagination, TotalCount total)
    {
        Data = data;
        Pagination = pagination;
        Total = total;
    }

    public static PagedResponse<TValue> Create(List<TValue> data, PaginationParameters pagination, int recordCount)
    {
        return new PagedResponse<TValue>(data, pagination, TotalCount.Create(recordCount, pagination.PageSize));
    }
}
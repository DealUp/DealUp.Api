namespace DealUp.Domain.Common;

public record PaginationParameters
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int SkipCount => PageSize * (PageNumber - 1);

    private PaginationParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public static PaginationParameters Create(int pageNumber, int pageSize)
    {
        return new PaginationParameters(pageNumber, pageSize);
    }
}
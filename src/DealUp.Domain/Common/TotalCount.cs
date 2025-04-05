namespace DealUp.Domain.Common;

public record TotalCount
{
    public int TotalRecords { get; private set; }
    public int TotalPages { get; private set; }

    private TotalCount(int totalRecords, int totalPages)
    {
        TotalRecords = totalRecords;
        TotalPages = totalPages;
    }

    public static TotalCount Create(int totalRecords, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        return new TotalCount(totalRecords, totalPages);
    }
}
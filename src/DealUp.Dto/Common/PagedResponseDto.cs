namespace DealUp.Dto.Common;

public record PagedResponseDto<TValue>(List<TValue> Data, int PageNumber, int PageSize, int TotalPages, int TotalRecords);
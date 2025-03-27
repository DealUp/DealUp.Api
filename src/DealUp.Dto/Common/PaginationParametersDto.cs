using System.ComponentModel.DataAnnotations;

namespace DealUp.Dto.Common;

public record PaginationParametersDto
{
    [Range(1, int.MaxValue, ErrorMessage = "PageNumber parameter must be between 1 and Int32.MaxValue.")]
    public required int PageNumber { get; set; } = 1;

    [Range(10, 100, ErrorMessage = "PageSize parameter must be greater than 10 and less than or equal to 100.")]
    public required int PageSize { get; set; } = 50;
}
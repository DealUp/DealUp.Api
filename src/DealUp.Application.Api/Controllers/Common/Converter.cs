using DealUp.Domain.Abstractions;
using DealUp.Domain.Common;
using DealUp.Dto.Common;

namespace DealUp.Application.Api.Controllers.Common;

public static class Converter
{
    public static PaginationParameters ToDomain(this PaginationParametersDto pagination)
    {
        return PaginationParameters.Create(pagination.PageNumber, pagination.PageSize);
    }

    public static PagedResponseDto<TDtoValue> ToPagedDto<TDomainValue, TDtoValue>(
        this PagedResponse<TDomainValue> pagedResponse,
        Func<IEnumerable<TDomainValue>, List<TDtoValue>> converter)
        where TDomainValue : EntityBase
    {
        return new PagedResponseDto<TDtoValue>(
            converter(pagedResponse.Data),
            pagedResponse.Pagination.PageNumber,
            pagedResponse.Pagination.PageSize,
            pagedResponse.Total.TotalPages,
            pagedResponse.Total.TotalRecords);
    }
}
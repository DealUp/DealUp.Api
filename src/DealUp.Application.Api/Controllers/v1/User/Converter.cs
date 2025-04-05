using DealUp.Domain.User;
using DealUp.Domain.User.Values;
using DealUp.Dto.v1.User;

namespace DealUp.Application.Api.Controllers.v1.User;

public static class Converter
{
    public static StartVerificationResponseDto ToDto(this StartVerificationResponse verificationResponse)
    {
        return new StartVerificationResponseDto
        {
            Success = verificationResponse.Success,
            Message = verificationResponse.Message
        };
    }

    public static UserDetailsDto ToDto(this UserDetails userDetails)
    {
        return new UserDetailsDto
        {
            Username = userDetails.Username,
            Status = userDetails.Status,
            HasSellerProfile = userDetails.HasSellerProfile
        };
    }
}
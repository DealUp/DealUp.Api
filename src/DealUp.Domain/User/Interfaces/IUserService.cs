using DealUp.Domain.User.Values;

namespace DealUp.Domain.User.Interfaces;

public interface IUserService
{
    public Task<StartVerificationResponse> SendVerificationEmailIfNeededAsync(Guid userId);
    public Task VerifyUserAsync(FinishVerificationRequest request);
    public Task<UserDetails> GetUserDetailsAsync(Guid userId);
}
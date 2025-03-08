namespace DealUp.Domain.User.Interfaces;

public interface IUserService
{
    public Task<StartVerificationResponse> SendVerificationEmailIfNeededAsync(Guid userId);
    public Task VerifyUserAsync(Guid userId, string token);
}
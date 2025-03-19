namespace DealUp.Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task SetUserStatusAsync(Guid userId, UserVerificationStatus userVerificationStatus);
    public Task<Guid> SaveUserAsync(User user);

    public Task<PendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type);
    public Task<Guid> SaveNewPendingConfirmationAsync(PendingConfirmation confirmation);
    public Task<Guid> UpdatePendingConfirmationAsync(PendingConfirmation confirmation);
}
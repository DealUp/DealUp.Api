namespace DealUp.Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<User?> GetUserByEmailAsync(string userEmail);
    public Task SetUserStatusAsync(Guid userId, Status status);
    public Task<bool> IsPendingConfirmationExistsAsync(Guid userId, ConfirmationType type);
    public Task<PendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type);
    public Task SetPendingConfirmationAsUsedAsync(Guid userId, ConfirmationType type);
    public Task<Guid> SaveUserAsync(User user);
    public Task<Guid> SavePendingConfirmationAsync(PendingConfirmation confirmation);
}
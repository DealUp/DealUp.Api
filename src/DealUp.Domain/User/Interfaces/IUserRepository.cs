namespace DealUp.Domain.User.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<Guid> SaveUserAsync(User user);

    public Task<UserPendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type);
    public Task<Guid> SaveNewPendingConfirmationAsync(UserPendingConfirmation confirmation);
    public Task<Guid> UpdatePendingConfirmationAsync(UserPendingConfirmation confirmation);
}
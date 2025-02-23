namespace DealUp.Domain.User;

public interface IUserRepository
{
    public Task<User?> GetUserAsync(string userEmail);
    public Task SaveUserAsync(User user);
}
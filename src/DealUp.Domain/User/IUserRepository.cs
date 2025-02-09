namespace DealUp.Domain.User;

public interface IUserRepository
{
    Task<User> GetUserAsync(string username);
}
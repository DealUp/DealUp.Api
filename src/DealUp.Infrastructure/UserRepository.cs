using DealUp.Domain.User;
using DealUp.Utils;

namespace DealUp.Infrastructure;

public class UserRepository : IUserRepository
{
    public Task<User> GetUserAsync(string username)
    {
        return Task.FromResult(new User("admin", "123".ToSha256()));
    }
}
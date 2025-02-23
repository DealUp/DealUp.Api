using DealUp.Domain.User;
using Microsoft.EntityFrameworkCore;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Database.Repositories.User;

public class UserRepository(DatabaseContext databaseContext) : IUserRepository
{
    public async Task<UserDomain?> GetUserAsync(string userEmail)
    {
        var user = await databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == userEmail);

        return user?.ToDomain();
    }

    public async Task SaveUserAsync(UserDomain user)
    {
        await databaseContext.AddAsync(user.ToDal());
        await databaseContext.SaveChangesAsync();
    }
}
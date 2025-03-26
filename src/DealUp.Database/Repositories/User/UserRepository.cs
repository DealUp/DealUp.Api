using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using DealUp.Domain.User.Values;
using Microsoft.EntityFrameworkCore;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Database.Repositories.User;

public class UserRepository(PostgresqlContext databaseContext) : IUserRepository
{
    public async Task<UserDomain?> GetUserByIdAsync(Guid userId)
    {
        return await databaseContext.Set<UserDomain>().FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<UserDomain?> GetUserByUsernameAsync(string username)
    {
        return await databaseContext.Set<UserDomain>().FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<Guid> SaveUserAsync(UserDomain user)
    {
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<UserPendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type)
    {
        return await databaseContext.Set<UserPendingConfirmation>()
            .Include(x => x.User)
            .Where(confirmation => confirmation.User.Id == userId && confirmation.Type == type && !confirmation.IsUsed)
            .OrderByDescending(v => v.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid> SaveNewPendingConfirmationAsync(UserPendingConfirmation confirmation)
    {
        await databaseContext.AddAsync(confirmation);
        await databaseContext.SaveChangesAsync();
        return confirmation.Id;
    }

    public async Task<Guid> UpdatePendingConfirmationAsync(UserPendingConfirmation confirmation)
    {
        databaseContext.Update(confirmation);
        await databaseContext.SaveChangesAsync();
        return confirmation.Id;
    }
}
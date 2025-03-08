using DealUp.Dal;
using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Database.Repositories.User;

public class UserRepository(DatabaseContext databaseContext) : IUserRepository
{
    public async Task<UserDomain?> GetUserByIdAsync(Guid userId)
    {
        var user = await databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        return user?.ToDomain();
    }

    public async Task<UserDomain?> GetUserByEmailAsync(string userEmail)
    {
        var user = await databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == userEmail);

        return user?.ToDomain();
    }

    public Task SetUserStatusAsync(Guid userId, Status status)
    {
        return databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.Status, status.ToString()));
    }

    public Task<bool> IsPendingConfirmationExistsAsync(Guid userId, ConfirmationType type)
    {
        return databaseContext.Set<UserPendingConfirmation>()
            .AsNoTracking()
            .Where(verification => verification.UserId == userId && verification.Type == type.ToString() && !verification.IsUsed)
            .AnyAsync();
    }

    public async Task<PendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type)
    {
        var pendingConfirmation = await databaseContext.Set<UserPendingConfirmation>()
            .AsNoTracking()
            .Where(verification => verification.UserId == userId && verification.Type == type.ToString() && !verification.IsUsed)
            .OrderByDescending(v => v.CreatedAt)
            .FirstOrDefaultAsync();

        return pendingConfirmation?.ToDomain();
    }

    public Task SetPendingConfirmationAsUsedAsync(Guid userId, ConfirmationType type)
    {
        return databaseContext.Set<UserPendingConfirmation>()
            .AsNoTracking()
            .Where(verification => verification.UserId == userId && verification.Type == type.ToString())
            .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.IsUsed, true));
    }

    public async Task<Guid> SaveUserAsync(UserDomain user)
    {
        await databaseContext.AddAsync(user.ToDal());
        await databaseContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<Guid> SavePendingConfirmationAsync(PendingConfirmation confirmation)
    {
        await databaseContext.AddAsync(confirmation.ToDal());
        await databaseContext.SaveChangesAsync();
        return confirmation.Id;
    }
}
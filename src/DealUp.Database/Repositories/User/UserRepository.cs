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

    public async Task<UserDomain?> GetUserByUsernameAsync(string username)
    {
        var user = await databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username);

        return user?.ToDomain();
    }

    public Task SetUserStatusAsync(Guid userId, UserVerificationStatus userVerificationStatus)
    {
        return databaseContext.Set<Dal.User>()
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.Status, userVerificationStatus.ToString()));
    }

    public async Task<Guid> SaveUserAsync(UserDomain user)
    {
        await databaseContext.AddAsync(user.ToDal());
        await databaseContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<PendingConfirmation?> GetPendingConfirmationAsync(Guid userId, ConfirmationType type)
    {
        var pendingConfirmation = await databaseContext.Set<UserPendingConfirmation>()
            .Where(verification => verification.UserId == userId && verification.Type == type.ToString() && !verification.IsUsed)
            .OrderByDescending(v => v.CreatedAt)
            .FirstOrDefaultAsync();

        return pendingConfirmation?.ToDomain();
    }

    public async Task<Guid> SaveNewPendingConfirmationAsync(PendingConfirmation confirmation)
    {
        await databaseContext.AddAsync(confirmation.ToDal());
        await databaseContext.SaveChangesAsync();
        return confirmation.Id;
    }

    public async Task<Guid> UpdatePendingConfirmationAsync(PendingConfirmation confirmation)
    {
        databaseContext.Update(confirmation.ToDal());
        await databaseContext.SaveChangesAsync();
        return confirmation.Id;
    }
}
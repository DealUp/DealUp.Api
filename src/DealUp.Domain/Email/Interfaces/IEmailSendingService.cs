using DealUp.Domain.User;

namespace DealUp.Domain.Email.Interfaces;

public interface IEmailSendingService
{
    public Task SendEmailVerificationAsync(PendingConfirmation pendingConfirmation);
}
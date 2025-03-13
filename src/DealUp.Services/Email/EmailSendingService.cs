using DealUp.Domain.Email;
using DealUp.Domain.Email.Interfaces;
using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using DealUp.EmailSender.Interfaces;
using DealUp.Infrastructure.Configuration.Options;
using Microsoft.Extensions.Options;

namespace DealUp.Services.Email;

public class EmailSendingService(IEmailSenderFactory emailSenderFactory, IUserRepository userRepository, IOptions<EmailSendingOptions> options) : IEmailSendingService
{
    public async Task SendEmailVerificationAsync(PendingConfirmation pendingConfirmation)
    {
        var user = await userRepository.GetUserByIdAsync(pendingConfirmation.UserId);

        string htmlBody = await BuildEmailVerificationBodyAsync(pendingConfirmation.Token, pendingConfirmation.UserId);
        var messageToSend = Message.Create(user!.Username, "Confirm your DealUp account", htmlBody);

        var emailSender = emailSenderFactory.GetEmailSender();
        await emailSender.SendMessageAsync(messageToSend);
    }

    private async Task<string> BuildEmailVerificationBodyAsync(string token, Guid userId)
    {
        var htmlBody = await File.ReadAllTextAsync(Path.Combine("EmailTemplates", "confirm-email.html"));
        var url = $"{options.Value.BaseUrl}/verify-email?token={Uri.EscapeDataString(token)}&id={Uri.EscapeDataString(userId.ToString())}";
        return htmlBody.Replace("{URL_MACROS}", url);
    }
}
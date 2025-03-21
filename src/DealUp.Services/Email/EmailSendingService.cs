using DealUp.Domain.Email;
using DealUp.Domain.Email.Interfaces;
using DealUp.Domain.User;
using DealUp.EmailSender.Interfaces;
using DealUp.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace DealUp.Services.Email;

public class EmailSendingService(IEmailSenderFactory emailSenderFactory, IOptions<EmailSendingOptions> options) : IEmailSendingService
{
    public async Task SendEmailVerificationAsync(UserPendingConfirmation userPendingConfirmation)
    {
        string htmlBody = await BuildEmailVerificationBodyAsync(userPendingConfirmation.Token, userPendingConfirmation.User.Id);
        var messageToSend = Message.Create(userPendingConfirmation.User.Username, "Confirm your DealUp account", htmlBody);

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
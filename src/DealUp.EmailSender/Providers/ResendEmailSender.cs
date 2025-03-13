using DealUp.Constants;
using DealUp.Domain.Email;
using DealUp.EmailSender.Interfaces;
using DealUp.Infrastructure.Configuration.Options;
using Microsoft.Extensions.Options;
using Resend;

namespace DealUp.EmailSender.Providers;

public class ResendEmailSender(IResend resendClient, IOptions<EmailSendingOptions> options) : IEmailSender
{
    public async Task SendMessageAsync(Message message)
    {
        var emailMessage = new EmailMessage
        {
            From = new EmailAddress
            {
                DisplayName = ProjectConstants.Name,
                Email = options.Value.FromEmailAddress
            },
            Subject = message.Subject,
            HtmlBody = message.HtmlBody
        };

        emailMessage.To.Add(message.TargetAddress);
        await resendClient.EmailSendAsync(emailMessage);
    }
}
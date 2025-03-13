using DealUp.EmailSender.Interfaces;
using DealUp.Infrastructure.Configuration.Options;
using Microsoft.Extensions.Options;

namespace DealUp.EmailSender;

public class EmailSenderFactory(IEnumerable<IEmailSender> emailSenders, IOptionsSnapshot<EmailSendingOptions> options) : IEmailSenderFactory
{
    public IEmailSender GetEmailSender()
    {
        return emailSenders.First(emailSender => emailSender.GetType().Name == $"{options.Value.ProviderName}Sender");
    }
}
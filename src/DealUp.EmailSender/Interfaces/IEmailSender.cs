using DealUp.Domain.Email;

namespace DealUp.EmailSender.Interfaces;

public interface IEmailSender
{
    public Task SendMessageAsync(Message message);
}
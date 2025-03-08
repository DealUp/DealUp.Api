namespace DealUp.EmailSender.Interfaces;

public interface IEmailSenderFactory
{
    public IEmailSender GetEmailSender();
}
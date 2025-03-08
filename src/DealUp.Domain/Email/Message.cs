namespace DealUp.Domain.Email;

public class Message
{
    public string TargetAddress { get; private set; }
    public string Subject { get; private set; }
    public string HtmlBody { get; private set; }

    private Message(string targetAddress, string subject, string htmlBody)
    {
        TargetAddress = targetAddress;
        Subject = subject;
        HtmlBody = htmlBody;
    }

    public static Message Create(string targetAddress, string subject, string htmlBody)
    {
        return new Message(targetAddress, subject, htmlBody);
    }
}
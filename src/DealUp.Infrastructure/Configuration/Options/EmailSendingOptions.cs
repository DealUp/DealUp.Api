namespace DealUp.Infrastructure.Configuration.Options;

public class EmailSendingOptions
{
    public const string SectionName = "EmailSendingOptions";

    public bool IsEnabled { get; set; }
    public int SecureTokenLength { get; set; } = 64;
    public string FromEmailAddress { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
}
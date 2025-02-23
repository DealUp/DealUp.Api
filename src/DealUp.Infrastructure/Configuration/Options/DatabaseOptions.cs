namespace DealUp.Infrastructure.Configuration.Options;

public class DatabaseOptions
{
    public const string SectionName = "DatabaseOptions";

    public required string ConnectionString { get; set; }
}
namespace DealUp.DataLake.Configuration;

public class DataLakeOptions
{
    internal const string SectionName = "DataLakeOptions";

    public string Mode { get; set; } = string.Empty;
    public required int MaxFileSizeInBytes { get; set; } = 10 * 1024 * 1024;
    public AmazonS3Options? AmazonS3Options { get; set; }
}

public class AmazonS3Options
{
    public required string Region { get; set; } = string.Empty;
    public required string BucketName { get; set; } = string.Empty;
    public required string AccessKeyId { get; set; } = string.Empty;
    public required string SecretAccessKey { get; set; } = string.Empty;
}
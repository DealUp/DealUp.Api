namespace DealUp.DataLake.AmazonS3.Models;

public record CreatePreSignedPostRequest
{
    public required string BucketName { get; set; }
    public required string Key { get; set; }
    public required TimeSpan Expires { get; set; }
    public required int MaxFileSizeInBytes { get; set; }
}
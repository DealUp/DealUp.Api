namespace DealUp.DataLake.AmazonS3.Models;

public record CreatePreSignedPostResponse
{
    public required Uri Url { get; set; }
    public required Dictionary<string, string> Fields { get; set; }
}
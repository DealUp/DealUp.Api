using Amazon.S3;
using Amazon.S3.Model;
using DealUp.DataLake.AmazonS3.Interfaces;
using DealUp.DataLake.AmazonS3.Models;
using DealUp.DataLake.Configuration;
using DealUp.DataLake.Interfaces;
using Microsoft.Extensions.Options;

namespace DealUp.DataLake.AmazonS3;

public class AmazonS3DataLake(IAmazonS3ExtendedClient s3Client, IOptions<DataLakeOptions> options) : IDataLake
{
    private readonly AmazonS3Options _amazonOptions = options.Value.AmazonS3Options!;

    public CreatePreSignedPostResponse GeneratePreSignedPost(string filePath, string fileName)
    {
        var request = new CreatePreSignedPostRequest
        {
            BucketName = _amazonOptions.BucketName,
            Key = IDataLake.CombinePath(filePath, fileName),
            Expires = TimeSpan.FromSeconds(5),
            MaxFileSizeInBytes = options.Value.MaxFileSizeInBytes
        };

        return s3Client.CreatePreSignedPost(request);
    }

    public Task<string> GeneratePreSignedGet(string objectKey)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _amazonOptions.BucketName,
            Key = objectKey,
            Expires = DateTime.UtcNow.AddSeconds(5),
            Verb = HttpVerb.GET
        };

        return s3Client.GetPreSignedURLAsync(request);
    }

    public async Task<List<string>> GetKeysByPrefixAsync(string prefix)
    {
        var request = new ListObjectsV2Request
        {
            BucketName = _amazonOptions.BucketName,
            Prefix = IDataLake.CombinePath(prefix)
        };

        var response = await s3Client.ListObjectsV2Async(request);
        return response.S3Objects.Select(s3Object => s3Object.Key).ToList();
    }
}
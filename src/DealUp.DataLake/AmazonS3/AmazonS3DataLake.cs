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

    public async Task<List<string>> GetKeysByPrefixAsync(string searchPrefix)
    {
        var request = new ListObjectsV2Request
        {
            BucketName = _amazonOptions.BucketName,
            Prefix = IDataLake.CombinePaths(searchPrefix)
        };

        var response = await s3Client.ListObjectsV2Async(request);
        return response.S3Objects.Select(s3Object => s3Object.Key).ToList();
    }

    public Task<string> GeneratePreSignedGetAsync(string objectKey)
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

    public Task<CreatePreSignedPostResponse> GeneratePreSignedPostAsync(string filePath)
    {
        var fileName = Path.GetRandomFileName();
        var request = new CreatePreSignedPostRequest
        {
            BucketName = _amazonOptions.BucketName,
            Key = IDataLake.CombinePaths(filePath, fileName),
            Expires = TimeSpan.FromSeconds(5),
            MaxFileSizeInBytes = options.Value.MaxFileSizeInBytes
        };

        return Task.FromResult(s3Client.CreatePreSignedPost(request));
    }
}
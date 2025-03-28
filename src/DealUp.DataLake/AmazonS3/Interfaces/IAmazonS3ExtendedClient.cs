using Amazon.S3;
using DealUp.DataLake.AmazonS3.Models;

namespace DealUp.DataLake.AmazonS3.Interfaces;

public interface IAmazonS3ExtendedClient : IAmazonS3
{
    public CreatePreSignedPostResponse CreatePreSignedPost(CreatePreSignedPostRequest request);
}
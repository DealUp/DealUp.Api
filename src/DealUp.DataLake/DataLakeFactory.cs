using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DealUp.DataLake.AmazonS3;
using DealUp.DataLake.Configuration;
using DealUp.DataLake.Interfaces;
using DealUp.DataLake.Models;
using Microsoft.Extensions.Options;

namespace DealUp.DataLake;

public class DataLakeFactory(IOptions<DataLakeOptions> options) : IDataLakeFactory
{
    public IDataLake CreateDataLake(DataLakeType dataLakeType)
    {
        return dataLakeType switch
        {
            DataLakeType.AmazonS3 => CreateAmazonS3DataLake(options),
            _ => throw new ArgumentOutOfRangeException(nameof(dataLakeType), dataLakeType, $"{dataLakeType.ToString()} is unsupported as DataLake provider.")
        };
    }

    private static IDataLake CreateAmazonS3DataLake(IOptions<DataLakeOptions> options)
    {
        var amazonS3Options = options.Value.AmazonS3Options;
        if (amazonS3Options is null)
        {
            throw new ArgumentNullException(nameof(amazonS3Options), "You must provide Amazon S3 options.");
        }

        var awsCredentials = new BasicAWSCredentials(amazonS3Options.AccessKeyId, amazonS3Options.SecretAccessKey);
        var awsConfiguration = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(amazonS3Options.Region)
        };

        var s3Client = new AmazonS3ExtendedClient(awsCredentials, awsConfiguration);
        return new AmazonS3DataLake(s3Client, options);
    }
}
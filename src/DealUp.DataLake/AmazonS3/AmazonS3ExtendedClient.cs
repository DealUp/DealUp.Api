using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Amazon.S3;
using DealUp.DataLake.AmazonS3.Interfaces;
using DealUp.DataLake.AmazonS3.Models;

namespace DealUp.DataLake.AmazonS3;

public class AmazonS3ExtendedClient(AWSCredentials credentials, AmazonS3Config configuration) : AmazonS3Client(credentials, configuration), IAmazonS3ExtendedClient
{
    public CreatePreSignedPostResponse CreatePreSignedPost(CreatePreSignedPostRequest request)
    {
        var regionName = Config.RegionEndpoint.SystemName;
        var credentials = Credentials.GetCredentials();
        var signingDate = Config.CorrectedUtcNow.ToString("yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture);
        var signingAlgorithm = "AWS4-HMAC-SHA256";
        var shortDate = signingDate[..8];
        var credentialScope = $"{shortDate}/{regionName}/s3/aws4_request";
        var parsedCredentials = $"{credentials.AccessKey}/{credentialScope}";
        var url = new Uri($"https://{request.BucketName}.s3.{regionName}.amazonaws.com");

        var conditions = new JsonArray
        {
            new JsonObject { ["bucket"] = request.BucketName },
            new JsonArray { "eq", "$key", request.Key },
            new JsonArray { "content-length-range", 0, request.MaxFileSizeInBytes },
            new JsonObject { ["X-Amz-Algorithm"] = signingAlgorithm },
            new JsonObject { ["X-Amz-Credential"] = parsedCredentials },
            new JsonObject { ["X-Amz-Date"] = signingDate }
        };

        var expiration = DateTime.UtcNow.Add(request.Expires).ToString("yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture);
        var policy = new PostPolicy
        {
            Expiration = expiration,
            Conditions = conditions
        };

        var policyJson = JsonSerializer.Serialize(policy);
        var policyEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(policyJson));

        var signingKey = AWS4Signer.ComposeSigningKey(credentials.SecretKey, regionName, shortDate, "s3");
        var signature = AWS4Signer.ComputeKeyedHash(SigningAlgorithm.HmacSHA256, signingKey, policyEncoded);

        var fields = new Dictionary<string, string>
        {
            { "Key", request.Key },
            { "Policy", policyEncoded },
            { "X-Amz-Signature", Convert.ToHexString(signature).ToLowerInvariant() },
            { "X-Amz-Algorithm", signingAlgorithm },
            { "X-Amz-Credential", parsedCredentials },
            { "X-Amz-Date", signingDate }
        };

        return new CreatePreSignedPostResponse
        {
            Url = url,
            Fields = fields
        };
    }
}
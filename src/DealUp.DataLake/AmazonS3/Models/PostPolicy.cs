using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DealUp.DataLake.AmazonS3.Models;

public record PostPolicy
{
    [JsonPropertyName("expiration")]
    public required string Expiration { get; set; }

    [JsonPropertyName("conditions")]
    public required JsonArray Conditions { get; set; }
}

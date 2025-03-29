using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.Extensions;

public static class PropertyBuilderExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerOptions.Default)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
                value => JsonSerializer.Serialize(value, SerializerOptions),
                value => JsonSerializer.Deserialize<TProperty>(value, SerializerOptions)!)
            .IsUnicode();
    }
}